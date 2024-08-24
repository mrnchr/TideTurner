using System.Collections.Generic;

namespace Muchachos.TideTurner.Runtime.Level.Obstacles.LifeCycle
{
    public interface ILevelUpdater
    {
        public void Construct(IEnumerable<ILevelUpdatable> updatable);
        
        void Add(ILevelUpdatable updatable);
        void AddRange(IEnumerable<ILevelUpdatable> updatable);
        void SetPause(bool value);
    }
}