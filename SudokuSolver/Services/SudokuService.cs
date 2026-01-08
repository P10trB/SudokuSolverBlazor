namespace SudokuSolver.Services;

public class SudokuService
    {
        private const int GRID_SIZE = 9;

        // Metoda publiczna wywoływana z widoku
        // Zmieniamy TextBox[,] na int?[,] (tablica liczb, gdzie null = puste pole)
        public bool SolveSudoku(int?[,] board)
        {
            for (int row = 0; row < GRID_SIZE; row++)
            {
                for (int column = 0; column < GRID_SIZE; column++)
                {
                    // Sprawdzamy czy pole jest puste (null)
                    // Wcześniej sprawdzałeś: !list.Contains(textBox.Text)
                    if (board[row, column] == null)
                    {
                        for (int number = 1; number <= GRID_SIZE; number++)
                        {
                            if (CanBePlaced(board, number, row, column))
                            {
                                // Przypisujemy liczbę do tablicy danych, nie do TextBoxa
                                board[row, column] = number;

                                if (SolveSudoku(board))
                                {
                                    return true;
                                }
                                else
                                {
                                    // Backtracking: czyścimy pole
                                    board[row, column] = null;
                                }
                            }
                        }
                        return false;
                    }
                }
            }
            return true;
        }

        // Metody pomocnicze operują teraz na tablicy int?[,] i szukają inta, a nie stringa

        private bool ExistsInRow(int?[,] board, int number, int row)
        {
            for (int i = 0; i < GRID_SIZE; i++)
            {
                if (board[row, i] == number)
                {
                    return true;
                }
            }
            return false;
        }

        private bool ExistsInColumn(int?[,] board, int number, int column)
        {
            for (int i = 0; i < GRID_SIZE; i++)
            {
                if (board[i, column] == number)
                {
                    return true;
                }
            }
            return false;
        }

        private bool ExistsInSquare(int?[,] board, int number, int row, int column)
        {
            int currentSquareRow = row - row % 3;
            int currentSquareColumn = column - column % 3;

            for (int i = currentSquareRow; i < currentSquareRow + 3; i++)
            {
                for (int j = currentSquareColumn; j < currentSquareColumn + 3; j++)
                {
                    if (board[i, j] == number)
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        private bool CanBePlaced(int?[,] board, int number, int row, int column)
        {
            return !ExistsInRow(board, number, row) &&
                   !ExistsInColumn(board, number, column) &&
                   !ExistsInSquare(board, number, row, column);
        }
    }