namespace AppiSimo.Client.Shared.Services
{
    using System;
    using System.Reactive.Subjects;

    public abstract class BaseRxService<T>
        where T : new()
    {
        readonly BehaviorSubject<T> _subject = new BehaviorSubject<T>(new T());

        public T Value => _subject.Value;

        public void Subscribe(IObserver<T> observer)
        {
            _subject.Subscribe(observer);
        }

        public void Subscribe(Action<T> onNext)
        {
            _subject.Subscribe(onNext);
        }

        public void OnNext(T pager)
        {
            _subject.OnNext(pager);
        }
    }
}