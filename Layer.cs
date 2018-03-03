using System;
using System.Collections.Generic;

namespace Neural {
    public class Layer {
        public List<Neuron> Neurons;
        public Layer(int Size) {
            Neurons = new List<Neuron>();
            for (int i = 0; i < Size; i++) {
                Neurons.Add(new Neuron());
            }
        }

    }
}