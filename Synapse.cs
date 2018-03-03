using System;

namespace Neural {
	public class Synapse {
		public Neuron sourceNeuron, targetNeuron;
		public double Weight, deltaWeight;

		public Synapse(Neuron sourceNeuron, Neuron targetNeuron) {
			this.sourceNeuron = sourceNeuron;
			this.targetNeuron = targetNeuron;
			sourceNeuron.Axons.Add(this);
			targetNeuron.Dendrites.Add(this);
			Weight = Network.rand.NextDouble() * 2 - 1;
		}
	}
}