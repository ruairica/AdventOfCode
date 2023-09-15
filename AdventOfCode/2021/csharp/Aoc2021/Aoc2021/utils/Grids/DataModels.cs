namespace Aoc2021.utils.Grids;


public record ValCoord(int Val, Coord Coord);

public record Coord(int x, int y);

public record GridItem<T>(int val, T otherVal);

public record GridItemCoord<T>(GridItem<T> gridItem, Coord Coord);
