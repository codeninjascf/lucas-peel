using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GridManager : MonoBehaviour
{
    public AudioManager audioManager;
    public Grid grid;
    
    
    [Header("Piece Setup")]
    public GameObject pieceBackground;
    public GameObject piecePrefab;
    public Sprite[] normalPieceSprites;

    public bool preFillGrid = true;

    public int Score => grid.Score;
    public bool GameActive { get; set; }
    public bool MadeMove { get; private set; }
    public bool MoveComplete { get; private set; }

    private Camera _camera;

    private Piece _heldPiece;
    private Piece _slidingPiece;

    private Piece _previouslyHeld;
    private Piece _previouslySlid;

    private static readonly Vector2Int[] AdjacentSides = {
        new (1, 0),
        new (-1, 0),
        new (0, 1),
        new (0, -1)
    };
    
    private void Start()
    {
        GameActive = true;
        
        grid.Initialise(piecePrefab, normalPieceSprites, audioManager);
        _camera = Camera.main;
        
        // Creates initial grid
        for (int x = 0; x < grid.width; x++)
        {
            for (int y = 0; y < grid.height; y++)
            {
                Instantiate(pieceBackground, grid.GetWorldPosition(x, y), Quaternion.identity, grid.transform);
                if (preFillGrid) grid.CreateRandomPiece(x, y);
            }
        }
        
        StartCoroutine(GameLoop());
    }

    private void Update()
    {
        MadeMove = false;
        
        if (!MoveComplete || !GameActive) return;
        
        // Sets the held piece to be the piece clicked by the player
        if (Input.GetMouseButtonDown(0))
        {
            Vector2 mousePos = _camera.ScreenToWorldPoint(Input.mousePosition);

            if (grid.XBounds.x < mousePos.x && mousePos.x < grid.XBounds.y && 
                grid.YBounds.x < mousePos.y && mousePos.y < grid.YBounds.y)
            {
                Vector2Int gridPos = grid.GetGridPosition(mousePos);
                _heldPiece = grid[gridPos.x, gridPos.y];
            }
        }

        // If a held piece is released, checks if a valid swap can be made
        if (Input.GetMouseButtonUp(0) && _heldPiece != null)
        {
            if (_slidingPiece != null)
            {
                MadeMove = true;
                
                // Swaps pieces
                Vector2Int originalPos = new(_heldPiece.X, _heldPiece.Y);
                Vector2Int slidingPos = new(_slidingPiece.X, _slidingPiece.Y);
                grid[originalPos.x, originalPos.y] = _slidingPiece;
                grid[slidingPos.x, slidingPos.y] = _heldPiece;

                List<Piece> matches = new();
                matches.AddRange(grid.GetMatches(slidingPos.x, slidingPos.y));
                matches.AddRange(grid.GetMatches(originalPos.x, originalPos.y));

                // Return pieces to original position if there are no available matches
                if (matches.Count == 0 && 
                    _heldPiece.Type != PieceType.Rainbow && 
                    _slidingPiece.Type != PieceType.Rainbow)
                {
                    MadeMove = false;
                    
                    grid[originalPos.x, originalPos.y] = _heldPiece;
                    grid[slidingPos.x, slidingPos.y] = _slidingPiece;
                    grid.StopAllCoroutines();
                    StartCoroutine(grid.ResetPiece(_slidingPiece));
                }

                // Handles swapping rainbow pieces in all 3 cases
                if (_slidingPiece.Type == PieceType.Rainbow && _heldPiece.Type == PieceType.Rainbow)
                {
                    audioManager.activate5.Play();
                    
                    for (int x = 0; x < grid.width; x++)
                    for (int y = 0; y < grid.height; y++)
                    {
                        grid.RemovePiece(x, y);
                    }
                }
                else if (_slidingPiece.Type == PieceType.Rainbow)
                {
                    audioManager.activate5.Play();

                    for (int x = 0; x < grid.width; x++)
                    for (int y = 0; y < grid.height; y++)
                    {
                        if (grid[x, y].Colour == _heldPiece.Colour)
                        {
                            grid[x, y].Type = _heldPiece.Type;
                            grid.RemovePiece(x, y);
                        }
                    }   
                    
                    grid.RemovePiece(originalPos.x, originalPos.y);
                }
                else if (_heldPiece.Type == PieceType.Rainbow)
                {
                    audioManager.activate5.Play();

                    for (int x = 0; x < grid.width; x++)
                    for (int y = 0; y < grid.height; y++)
                    {
                        if (grid[x, y].Colour == _slidingPiece.Colour)
                        {
                            grid[x, y].Type = _heldPiece.Type;
                            grid.RemovePiece(x, y);
                        }
                    } 
                    
                    grid.RemovePiece(slidingPos.x, slidingPos.y);
                }
            }
            
            // Resets held and sliding piece
            _heldPiece.transform.position = grid.GetWorldPosition(_heldPiece.X, _heldPiece.Y);
            _previouslyHeld = _heldPiece;
            _previouslySlid = _slidingPiece;
            _heldPiece = null;
            _slidingPiece = null;
            MoveComplete = false;
        }

        // Makes held piece follow the mouse cursor, and slides pieces to give input interactivity
        if (_heldPiece != null)
        {
            // Move held piece to mouse position
            Vector3 mousePos = _camera.ScreenToWorldPoint(Input.mousePosition);
            mousePos.z = -1;
            _heldPiece.transform.position = mousePos;

            // Calculate relative position from mouse to piece location
            Vector3 relativePosition = mousePos - grid.GetWorldPosition(_heldPiece.X, _heldPiece.Y);

            // Reset sliding piece if held piece too close
            if (Mathf.Abs(relativePosition.x) < .5f && Mathf.Abs(relativePosition.y) < .5f)
            {
                if (_slidingPiece == null) return;
                
                grid.StopAllCoroutines();
                StartCoroutine(grid.ResetPiece(_slidingPiece));
                _slidingPiece = null;
                return;
            }
            
            // Finds the current side that the sliding piece should be on
            int sideIndex;
            if (Mathf.Abs(relativePosition.x) > Mathf.Abs(relativePosition.y))
            {
                sideIndex = relativePosition.x > 0 ? 0 : 1;
            }
            else
            {
                sideIndex = relativePosition.y > 0 ? 2 : 3;
            }
            Vector2Int sidePieceCoords = new Vector2Int(_heldPiece.X, _heldPiece.Y) + AdjacentSides[sideIndex];

            // Returns if side of sliding piece is out of bounds
            if (sidePieceCoords.x < 0 || sidePieceCoords.x >= grid.width ||
                sidePieceCoords.y < 0 || sidePieceCoords.y >= grid.height)
            {
                return;
            }

            // Finds the new sliding piece, and slides it accordingly, resetting the previous piece if necessary
            Piece newSlidingPiece = grid[sidePieceCoords.x, sidePieceCoords.y];
            if (_slidingPiece != null && _slidingPiece != newSlidingPiece)
            {
                grid.StopAllCoroutines();
                StartCoroutine(grid.ResetPiece(_slidingPiece));
                _slidingPiece = null;
            }
            _slidingPiece = newSlidingPiece;
            grid.StartCoroutine(grid.MovePiece(newSlidingPiece, _heldPiece.X, _heldPiece.Y));
        }
    }

    // Main game loop for all the grid's operations
    // Runs operations on a delayed loop so that it is synchronised with the grid's movement
    private IEnumerator GameLoop()
    {
        while (GameActive)
        {
            // Checks if any pieces are currently in need of being dropped
            if (!grid.DropPieces())
            {
                // Finds the match data for all pieces in the grid
                List<(int count, List<Piece> matches, Piece root)> gridMatchData = new();
                List<Piece> usedPieces = new();

                bool matchesFound = false;
                
                for (int x = 0; x < grid.width; x++)
                for (int y = 0; y < grid.height; y++)
                {
                    List<Piece> matches = grid.GetMatches(x, y);

                    if (matches.Count == 0) continue;
                    
                    matchesFound = true;
                    gridMatchData.Add((matches.Count, matches, grid[x, y]));
                }

                // Organises matches in descending order by their count and loops through them
                foreach ((int count, List<Piece> matches, Piece piece) in 
                    gridMatchData.OrderByDescending(m => m.count))
                {
                    // If a match hasn't yet been processed, it is handled correctly to find all the pieces which have
                    // been matched, and to create pieces in the case where a match has a size greater than 3
                    if (!usedPieces.Contains(piece))
                    {
                        bool fromHeldMatch = matches.Contains(_previouslyHeld) && _previouslyHeld != null;
                        bool fromSlidingMatch = matches.Contains(_previouslySlid) && _previouslySlid != null;

                        Piece root = fromSlidingMatch ? _previouslySlid : fromHeldMatch ? _previouslyHeld : piece;
                        
                        usedPieces.Add(root);
                        grid.RemovePiece(root.X, root.Y);

                        switch (count)
                        {
                            case 3:
                                audioManager.match3.Play();
                                break;
                            case 4:
                                audioManager.match4.Play();
                                grid.CreatePiece(root.X, root.Y, piece.Colour,
                                    Random.Range(0, 2) == 0 ? PieceType.Horizontal : PieceType.Vertical);
                                break;
                            case >= 5:
                                audioManager.match5.Play();
                                grid.CreatePiece(root.X, root.Y, piece.Colour, PieceType.Rainbow);
                                break;
                        }
                    }
                    
                    // Removes all matched pieces from the grid
                    foreach (Piece match in matches.Where(match => !usedPieces.Contains(match)))
                    {
                        grid.RemovePiece(match.X, match.Y);
                        usedPieces.Add(match);

                        if (matches.Contains(_previouslySlid))
                        {
                            _previouslySlid = null;
                        }

                        if (matches.Contains(_previouslyHeld))
                        {
                            _previouslyHeld = null;
                        }
                    }
                }
                
                MoveComplete = !matchesFound;
            }
            else
            {
                MoveComplete = false;
            }
            yield return new WaitForSeconds(grid.fillTime);
        }
    }
}
