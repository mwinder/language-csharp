using System;
using System.Collections.Generic;

namespace Language
{
    class Variance
    {
        static void Main()
        {
            IEnumerable<Abstraction> list1 = new List<Concretion>();
            // Will not work. List is not covariant:
            // List<Abstraction> list2 = new List<Concretion> { new Concretion(), new Concretion() };

            IOut<Abstraction> covariant = new CovariantParameterisedType<Concretion>();

            IIn<Concretion> contravariant = new ContravariantParameterisedType<Abstraction>();

            Func<Abstraction> func = () => new Concretion();

            Action<Concretion> action = new Action<Abstraction>(delegate(Abstraction x) { });

            Console.ReadLine();
        }

        public interface Abstraction
        {
        }

        public class Concretion : Abstraction
        {
        }

        public interface IOut<out T>
        {
        }

        public class CovariantParameterisedType<T> : IOut<T>
        {
        }

        public interface IIn<in T>
        {
        }

        public class ContravariantParameterisedType<T> : IIn<T>
        {
        }
    }
}
