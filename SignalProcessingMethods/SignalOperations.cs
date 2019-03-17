﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MathNet.Numerics;
using SignalProcessingCore;

namespace SignalProcessingMethods
{
    public class SignalOperations
    {
        public static List<double> AddSignals(List<double> signal1, List<double> signal2)
        {
            List<double> result = new List<double>();
            for (int i = 0; i < signal1.Count; i++)
            {
                result.Add(signal1[i] + signal2[i]);
            }

            return result;

        }

        public static List<double> SubtractSignals(List<double> signal1, List<double> signal2)
        {
            List<double> result = new List<double>();
            for (int i = 0; i < signal1.Count; i++)
            {
                result.Add(signal1[i] - signal2[i]);
            }

            return result;

        }

        public static List<double> MultiplySignals(List<double> signal1, List<double> signal2)
        {
            List<double> result = new List<double>();
            for (int i = 0; i < signal1.Count; i++)
            {
                result.Add(signal1[i] * signal2[i]);
            }

            return result;

        }

        public static List<double> DivideSignals(List<double> signal1, List<double> signal2)
        {
            List<double> result = new List<double>();
            for (int i = 0; i < signal1.Count; i++)
            {
                result.Add(signal1[i] / signal2[i]);
            }

            return result;

        }

        public static double AvgSignal(double t1, double t2, List<double> samples, bool isDiscrete = false)
        {
            if (isDiscrete)
            {
                return 1 / (t2 - t1 + 1) * Sum(Math.Abs((t2 - t1) / samples.Count), samples);
            }
            return 1 / (t2 - t1) * Integral(Math.Abs((t2 - t1) / samples.Count), samples);
        }

        public static double SignalVariance(double t1, double t2, List<double> samples, bool isDiscrete = false)
        {
            if (isDiscrete)
            {
                return 1 / (t2 - t1 + 1) * Sum(Math.Abs((t2 - t1) / samples.Count), samples, d => Math.Pow(d - AvgSignal(t1, t2, samples, true), 2));
            }
            return 1 / (t2 - t1) * Integral(Math.Abs((t2 - t1) / samples.Count), samples, d => Math.Pow(d - AvgSignal(t1, t2, samples), 2));

        }
        public static double AbsAvgSignal(double t1, double t2, List<double> samples, bool isDiscrete = false)
        {
            if (isDiscrete)
            {
                return 1 / (t2 - t1 + 1) * Sum(Math.Abs((t2 - t1) / samples.Count), samples, Math.Abs);
            }
            return 1 / (t2 - t1) * Integral(Math.Abs((t2 - t1) / samples.Count), samples, Math.Abs);
        }

        public static double AvgSignalPower(double t1, double t2, List<double> samples, bool isDiscrete = false)
        {
            if (isDiscrete)
            {
                return 1 / (t2 - t1 + 1) * Sum(Math.Abs((t2 - t1) / samples.Count), samples, d => d * d);
            }
            return 1 / (t2 - t1) * Integral(Math.Abs((t2 - t1) / samples.Count), samples, d => d * d);
        }

        public static double RMSSignal(double t1, double t2, List<double> samples, bool isDiscrete = false)
        {
            if (isDiscrete)
            {
                return Math.Sqrt(AvgSignalPower(t1, t2, samples, true));
            }
            return Math.Sqrt(AvgSignalPower(t1, t2, samples));
        }

        private static double Integral(double dx, List<double> samples, Func<double, double> additionalFunc = null)
        {
            double integral = 0;
            foreach (var sample in samples)
            {
                if (additionalFunc != null)
                    integral += additionalFunc(sample);
                else
                    integral += sample;
            }
            integral *= dx;

            return integral;
        }

        private static double Sum(double dx, List<double> samples, Func<double, double> additionalFunc = null)
        {
            double sum = 0;
            foreach (var sample in samples)
            {
                if (additionalFunc != null)
                    sum += additionalFunc(sample);
                else
                    sum += sample;
            }
            return sum * dx;
        }
    }
}
