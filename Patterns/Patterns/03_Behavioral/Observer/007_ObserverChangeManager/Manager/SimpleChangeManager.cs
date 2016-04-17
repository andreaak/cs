using System.Collections.Generic;

namespace Patterns._03_Behavioral.Observer._007_ObserverChangeManager.Manager
{
    // Простой менеджер изменений. 
    // Не имеет открытого конструктора. 
    class SimpleChangeManager : ChangeManager
    {
        static SimpleChangeManager singleton = new SimpleChangeManager();

        public static SimpleChangeManager Singleton
        {
            get { return singleton; }
        }

        private SimpleChangeManager()
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

        // Уведомление Всех подписчиков у Всех издателей.
        // Если один и тот же подписчик был подписан на несколько издателей он будет уведомлен несколько раз,
        // этот недостаток исключает DAGChangeManager.
        public override void Notify()
        {
            foreach (var m in mapping)
                foreach (var l in m.Value)
                    l.Update(m.Key);
        }
    }
}
