using System;
using System.Collections.Generic;
using System.Linq;

namespace NetworkModels {
	public class Network {
		public static double learnRate = .3, minimumError = .01;
		public List<Layer> Layers;
		public static Random rand = new Random();
		public Network(int[] Sizes) {
			Layers = new List<Layer>();

			for (var i = 0; i < Sizes.Length; i++)
				Layers.Add(new Layer(Sizes[i])); //add layers of neurons

			for (int i = 0; i < Layers.Count - 1; i++) //add synapses
				Layers[i].Neurons.ForEach(nFrom =>
					Layers[i + 1].Neurons.ForEach(nTo => {
						Synapse s = new Synapse(nFrom, nTo);
					}));
		}
		public void Train(List<DataSet> dataSets, double ? minimumError = .01, int ? epochs = null) {
			var error = 1.0;
			var numEpochs = 0;
			while (error > minimumError && (numEpochs < (epochs == null? int.MaxValue : epochs))) {
				var errors = new List<double>();
				dataSets.ForEach(dataSet => {
					ForwardPropagate(dataSet.Values);
					BackPropagate(dataSet.Targets);

					var i = 0;
					errors.Add(Layers.Last().Neurons.Sum(a => Math.Abs(dataSet.Targets[i++] - a.value)));
				});
				error = errors.Average();
				numEpochs++;
			}
		}
		private void ForwardPropagate(double[] inputs) {
			var i = 0;
			Layers.First().Neurons.ForEach(neuron => neuron.value = inputs[i++]);
			Layers.Where(lyr => lyr != Layers.First())
				.ToList().ForEach(lyr => lyr.Neurons.ForEach(neuron => neuron.Value()));
		}

		private bool isHiddenLayer(Layer lyr){
			return !(lyr == Layers.First() || lyr == Layers.Last());
		}
		private void BackPropagate(double[] targets) {
			var i = 0;
			Layers.Last().Neurons.ForEach(neuron => neuron.Gradient(targets[i++]));

			Layers.Reverse();
			Layers.Where(lyr => isHiddenLayer(lyr)).ToList()
				.ForEach(lyr => lyr.Neurons.ForEach(neuron => neuron.Gradient()));
			Layers.Where(lyr => isHiddenLayer(lyr)).ToList()
				.ForEach(lyr => lyr.Neurons.ForEach(neuron => neuron.UpdateWeights()));
			Layers.Reverse();

			Layers.Last().Neurons.ForEach(neuron => neuron.UpdateWeights());
		}

		public double[] Feed(double[] inputs) {
			ForwardPropagate(inputs);
			return Layers.Last().Neurons.Select(a => a.value).ToArray();
		}

		
	}

}