using System;
using System.Collections.Generic;
using System.Linq;
namespace NetworkModels {
    class Program {
        static void Main(string[] args) {
            Network n = new Network(new int[] { 1, 10, 1 });

            List<DataSet> ds = new List<DataSet>();
            for (int i = 1; i <= 5; i++)
                ds.Add(new DataSet(new double[] {
                    (double) i / 100
                }, new double[] {
                    (double) i * i / 100
                }));

            n.Train(ds, 0.01);
            while (true) {
                Console.WriteLine("give inputs:\n");
                string inputs = Console.ReadLine();
                if (inputs == "")
                    continue;
                var outt = n.Feed(new double[] { Convert.ToDouble(inputs) / 100 });
                outt.ToList().ForEach(d =>
                    Console.WriteLine("{0} , [{1}]", Math.Round(d * 100), d));

            }
        }
    }
}