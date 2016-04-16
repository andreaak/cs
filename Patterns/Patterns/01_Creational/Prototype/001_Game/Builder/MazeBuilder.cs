namespace Patterns._01_Creational.Prototype._001_Game.Builder
{
    // MazeBuilder - не создает лабиринты самостоятельно, его основная цель - просто определить интерфейс для создания лабиринтов.
    // Пустые реализации в этом интерфейсе определены для удобства.
    // Реальную работу выполняют подклассы MazeBuilder.
    class MazeBuilder
    {        
        // Конструктор.
        public MazeBuilder()
        {
        }

        public virtual void BuildMaze()
        {                        
        }

        public virtual void BuildRoom(int roomNo)
        {
        }

        public virtual void BuildDoor(int roomFrom, int roomTo)
        {
        }

        public virtual Maze GetMaze()
        {
            return null;
        }
    }
}
