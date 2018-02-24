using System;

namespace NetworkModels {
	public enum ActivatorFunc {
		Sigmoid,
		TanHyperbolic,
		SoftStep,
		Identity,
		ReLU
	}

	public class transferFunc {

		ActivatorFunc tF = ActivatorFunc.Sigmoid;
		public transferFunc(ActivatorFunc ? _tF) {
			tF = _tF ?? ActivatorFunc.Sigmoid;
		}

		public double activate(double x, ActivatorFunc ? _tF) {
			ActivatorFunc __tF = _tF ?? tF;
			switch (__tF) {
				case ActivatorFunc.Sigmoid:
					return Activate.Sigmoid(x);
				case ActivatorFunc.TanHyperbolic:
					return Activate.Tanh(x);
				case ActivatorFunc.Identity:
					return Activate.Identity(x);
				case ActivatorFunc.ReLU:
					return Activate.ReLU(x);
				case ActivatorFunc.SoftStep:
					return Activate.SoftStep(x);
			}
			return -1;
		}
	}
	public static class Activate {

		public static double Sigmoid(double x) {
			return (x<-45.0) ? 0.0 : (x> 45.0) ? 1.0 :
				1.0 / (1.0 + Math.Exp(-x));

		}
		public static double sigmoidDerivative(double x) {
			return x * (1 - x);
		}
		public static double Tanh(double x) {
			return (x<-10.0) ? -1.0 : (x> 10.0) ? 1.0 :
				Math.Tanh(x);
		}
		public static double tanhDerivative(double x) {
			return (1 - Math.Pow(Math.Tanh(x), 2));
		}

		public static double ReLU(double x) {
			return (x < 0.0) ? 0.0 : x;
		}

		public static double ReLUDerivative(double x) {
			return (x < 0.0) ? 0.0 : 1.0;
		}

		public static double Identity(double x) {
			return x;
		}

		public static double identityDerivative(double x) {
			return 1;
		}
		public static double SoftStep(double x) {
			return (x < 0.0) ? 0.0 : 1.0;
		}
		public static double SoftStepDerivative(double x) {
			return 0.0;
		}

	}
}