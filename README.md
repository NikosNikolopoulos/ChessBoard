# Chess Console Application

Welcome to the Chess Console Application! 

This simple console-based chess game allows two players to make moves on an 8x8 chessboard.

- [x] The application follows standard chess rules.
- [x] Players take turns making valid moves until one player achieves checkmate.
- [x] The game will display error messages for invalid moves.

## Board Visualization

<table border="1" cellspacing="0" cellpadding="10">
  <tr>
    <td></td>
    <td>A</td>
    <td>B</td>
    <td>C</td>
    <td>D</td>
    <td>E</td>
    <td>F</td>
    <td>G</td>
    <td>H</td>
  </tr>
  <tr>
    <td>1</td>
    <td>♖</td>
    <td>♘</td>
    <td>♗</td>
    <td>♕</td>
    <td>♔</td>
    <td>♗</td>
    <td>♘</td>
    <td>♖</td>
  </tr>
  <tr>
    <td>2</td>
    <td>♙</td>
    <td>♙</td>
    <td>♙</td>
    <td>♙</td>
    <td>♙</td>
    <td>♙</td>
    <td>♙</td>
    <td>♙</td>
  </tr>
  <tr>
    <td>3</td>
    <td class="empty"></td>
    <td class="empty"></td>
    <td class="empty"></td>
    <td class="empty"></td>
    <td class="empty"></td>
    <td class="empty"></td>
    <td class="empty"></td>
    <td class="empty"></td>
  </tr>
  <tr>
    <td>4</td>
    <td class="empty"></td>
    <td class="empty"></td>
    <td class="empty"></td>
    <td class="empty"></td>
    <td class="empty"></td>
    <td class="empty"></td>
    <td class="empty"></td>
    <td class="empty"></td>
  </tr>
  <tr>
    <td>5</td>
    <td class="empty"></td>
    <td class="empty"></td>
    <td class="empty"></td>
    <td class="empty"></td>
    <td class="empty"></td>
    <td class="empty"></td>
    <td class="empty"></td>
    <td class="empty"></td>
  </tr>
  <tr>
    <td>6</td>
    <td class="empty"></td>
    <td class="empty"></td>
    <td class="empty"></td>
    <td class="empty"></td>
    <td class="empty"></td>
    <td class="empty"></td>
    <td class="empty"></td>
    <td class="empty"></td>
  </tr>
  <tr>
    <td>7</td>
    <td>♟</td>
    <td>♟</td>
    <td>♟</td>
    <td>♟</td>
    <td>♟</td>
    <td>♟</td>
    <td>♟</td>
    <td>♟</td>
  </tr>
  <tr>
    <td>8</td>
    <td>♜</td>
    <td>♞</td>
    <td>♝</td>
    <td>♛</td>
    <td>♚</td>
    <td>♝</td>
    <td>♞</td>
    <td>♜</td>
  </tr>
</table>

## File Structure
```
ChessBoard/
│
├── Constants/
│   ├── Consts.cs
│
├── Coordinates/
│   ├── Coordinate.cs
│   ├── Mappings.cs
│
├── Logic/
│   ├── Utils.cs
│   ├── ImportExport.cs
│   ├── Piece.cs
│   ├── Checks.cs
│
├── Board/
│   ├── ChessBoard.cs
│
├── IO/
│   ├── IO.cs
│
├── Program/
│   ├── Chess.cs
```

- **Constants/:** Contains the enumerations used in the application.
- **Coordinates/:** Manages coordinate-related functionality.
- **Logic/:** Contains utility functions, the `ImportExport` record, and static methods for checking legal moves and piece paths.
- **Board/:** Manages the `ChessBoard` class and related functionality.
- **IO/:** Handles input/output operations.
- **Program/:** Contains the main program logic.
