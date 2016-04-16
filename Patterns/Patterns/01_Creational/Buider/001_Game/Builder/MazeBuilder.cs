namespace Patterns._01_Creational.Buider._001_Game.Builder
{
    // MazeBuilder - не создает лабиринты самостоятельно, его основная цель - просто определить интерфейс для создания лабиринтов.
    // Пустые реализации в этом интерфейсе определены для удобства.
    // Реальную работу выполняют подклассы MazeBuilder.
    abstract class MazeBuilder
    {
        public abstract void BuildMaze();

        public abstract void BuildRoom(int roomNo);

        public abstract void BuildDoor(int roomFrom, int roomTo);

        public abstract Maze GetMaze();
    }
}
