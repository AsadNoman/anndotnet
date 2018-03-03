using System;
using System.Collections.Generic;
using System.Linq;

namespace Neural {
	public class Neuron {
		public List<Synapse> Dendrites, Axons;
		public double bias, deltaBias, gradient, value;
		public Neuron() {
			Dendrites = new List<Synapse>();
			Axons = new List<Synapse>();
			bias = Network.rand.NextDouble() * 2 - 1;
		}
		public double Value() {
			return value = Activate.Sigmoid(Dendrites.Sum(a => a.Weight * a.sourceNeuron.value) + bias);
		}
		public double Gradient(double? target = null) {
			if (target == null)
				return gradient = Axons.Sum(a => a.targetNeuron.gradient * a.Weight) * Activate.sigmoidDerivative(value);

			return gradient = (target.Value - value) * Activate.sigmoidDerivative(value);
		}

		public void UpdateWeights() {
			bias += deltaBias;
			deltaBias = Network.learnRate * gradient;
			bias += deltaBias;

			Dendrites.ForEach(synapse => {
				synapse.Weight += synapse.deltaWeight;
				synapse.deltaWeight = Network.learnRate * gradient * synapse.sourceNeuron.value;
				synapse.Weight += synapse.deltaWeight;
			});
		}
	}
}