using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Patterns.Structural.Adapter._001_Cats.Superheroes
{
    class SuperheroAdapter : ISuperhero
    {
        private IFly _fly;
        private IShoot _shoot;
        private IWalls _walls;

        public SuperheroAdapter(IFly fly, IShoot shoot, IWalls walls)
        {
            _fly = fly;
            _shoot = shoot;
            _walls = walls;
        }

        public void Shoot()
        {
            _shoot.Shoot();
        }

        public void Fly()
        {
            _fly.Fly();
        }

        public void GoThrougWalls()
        {
            _walls.GoThrougWalls();
        }
    }
}
