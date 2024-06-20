using System.Collections.Generic;
using System.Linq;
using Zenject;

namespace Muchachos.TideTurner.Runtime.Level.Obstacles.LifeCycle
{
    public class LevelUpdater : ITickable, IFixedTickable, ILevelUpdater
    {
        private readonly List<ILevelUpdatable> _updatables = new List<ILevelUpdatable>();
        private bool _isPaused;

        public void Add(ILevelUpdatable updatable)
        {
            _updatables.Add(updatable);
        }

        public void AddRange(IEnumerable<ILevelUpdatable> updatable)
        {
            _updatables.AddRange(updatable);
        }

        public void SetPause(bool value)
        {
            _isPaused = value;
        }

        public void FixedTick()
        {
            if(!_isPaused)
                FixedUpdateLogic();
        }

        public void Tick()
        {
            if (!_isPaused)
                UpdateLogic();
        }

        private void UpdateLogic()
        {
            var copy = new List<IUpdatable>(_updatables.OfType<IUpdatable>());
            foreach (IUpdatable updatable in copy)
                updatable.UpdateLogic();
        }

        private void FixedUpdateLogic()
        {
            var copy = new List<IFixedUpdatable>(_updatables.OfType<IFixedUpdatable>());
            foreach (IFixedUpdatable updatable in copy)
                updatable.FixedUpdateLogic();
        }
    }
}