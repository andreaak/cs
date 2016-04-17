using System;
using System.Collections.Generic;
using System.Linq;

namespace Patterns._03_Behavioral.Observer._007_ObserverChangeManager.Manager
{
    class DAGChangeManager : ChangeManager, IEqualityComparer<Tuple<Subject.Subject, Observer.Observer>>
    {
        static DAGChangeManager singleton = new DAGChangeManager();

        public static DAGChangeManager Singleton
        {
            get { return singleton; }
        }

        private DAGChangeManager()
        {
        }

        // Регистрация подписчика в списке издателя.
        public override void Register(Subject.Subject subject, Observer.Observer observer)
        {
            if (!mapping.ContainsKey(subject))
                mapping.Add(subject, new List<Observer.Observer>());

            mapping[subject].Add(observer);
        }

        // Удаление подписчика из списка издателя.
        public override void Unregister(Subject.Subject subject, Observer.Observer observer)
        {
            if (!mapping.ContainsKey(subject))
                return;

            mapping[subject].Remove(observer);
        }

        // Уведомление подписчика только 1 раз вне зависимости от того, на скольких издателей он был подписан.
        public override void Notify()
        {
            // Выбор кортежей, в которых конкретный экземпляр типа Observer встречается только 1 раз.
            var tuples = mapping.SelectMany(kv => kv.Value.Select(observer => new Tuple<Subject.Subject, Observer.Observer>(kv.Key, observer))).Distinct(this);

            // Уведомление подписчиков.
            foreach (var tuple in tuples)
                tuple.Item2.Update(tuple.Item1);
        }
        
        // Реализация интерфейса IEqualityComparer<Tuple<Subject, Observer>>

        bool IEqualityComparer<Tuple<Subject.Subject, Observer.Observer>>.Equals(Tuple<Subject.Subject, Observer.Observer> x, Tuple<Subject.Subject, Observer.Observer> y)
        {
            return x.Item2.Equals(y.Item2);
        }

        int IEqualityComparer<Tuple<Subject.Subject, Observer.Observer>>.GetHashCode(Tuple<Subject.Subject, Observer.Observer> tuple)
        {
            return tuple.Item2.GetHashCode();
        }
    }
}
