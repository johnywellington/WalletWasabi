using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WalletWasabi.Helpers;

namespace WalletWasabi.Crypto.ZeroKnowledge
{
	public static class ZkVerifier
	{
		public static bool Verify(ZkKnowledgeOfExponent proof, GroupElement publicPoint, GroupElement generator)
		{
			Guard.False($"{nameof(generator)}.{nameof(generator.IsInfinity)}", generator.IsInfinity);
			Guard.False($"{nameof(publicPoint)}.{nameof(publicPoint.IsInfinity)}", publicPoint.IsInfinity);

			if (publicPoint == proof.RandomPoint)
			{
				throw new InvalidOperationException($"{nameof(publicPoint)} and {nameof(proof.RandomPoint)} should not be equal.");
			}

			var zkor = new ZkKnowledgeOfRepresentation(proof.RandomPoint, new[] { proof.Response });
			return Verify(zkor, publicPoint, new[] { generator });
		}

		public static bool Verify(ZkKnowledgeOfRepresentation proof, GroupElement publicPoint, IEnumerable<GroupElement> generators)
		{
			foreach (var generator in generators)
			{
				Guard.False($"{nameof(generator)}.{nameof(generator.IsInfinity)}", generator.IsInfinity);
			}

			var randomness = proof.Randomness;
			var responses = proof.Responses.ToArray();

			var challenge = ZkChallenge.HashToScalar(new[] { publicPoint, randomness }.Concat(generators).ToArray());
			var a = challenge * publicPoint + randomness;

			var b = GroupElement.Infinity;
			for (int i = 0; i < responses.Length; i++)
			{
				var response = responses[i];
				var generator = generators.ToArray()[i];

				b += response * generator;
			}
			return a == b;
		}
	}
}
