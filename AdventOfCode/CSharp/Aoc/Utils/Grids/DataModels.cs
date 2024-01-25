namespace Utils.Grids;


public record ValCoord<T>(T Val, Coord Coord);

public record Coord(int r, int c);

public record CoordL(long r, long c);

public record GridItem<T>(int val, T otherVal);

public record GridItemCoord<T>(GridItem<T> gridItem, Coord Coord);
