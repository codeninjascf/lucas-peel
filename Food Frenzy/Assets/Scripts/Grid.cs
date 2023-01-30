using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grid : MonoBehaviour
{
    // Score settings
    private const int NormalScore = 300;
    private const int ClearScore = 600;
    private const int RainbowScore = 1000;

    // Grid settings
    public Vector2 position;
    
    public int width;
    public int height;
    public float fillTime = 0.1f;
    
    // Interface for the grid of piece objects
    private Piece[,] _pieces;
    public Piece this[int x, int y]
    {
        get => _pieces[x, y];
        set
        {
            value.X = x;
            value.Y = y;
            _pieces[x, y] = value;
        }
    }
    
    // Score achieved for a grid
    public int Score { get; set; }

    // X and Y boundaries for the grid, to be used when determining if a world position lies within the grid
    public Vector2 XBounds { get; private set; }
    public Vector2 YBounds { get; private set; }
    
    // Values to be passed in during initialisation
    private GameObject _piecePrefab;
    private Sprite[] _sprites;
    private AudioManager _audioManager;

    // Initialises all necessary parts of a grid
    public void Initialise(GameObject piecePrefab, Sprite[] sprites, AudioManager audioManager)
    {
        _pieces = new Piece[width, height + 1];

        Vector2 minPoint = GetWorldPosition(0, 0);
        Vector2 maxPoint = GetWorldPosition(width - 1, height - 1);
        XBounds = new Vector2(minPoint.x - .5f, maxPoint.x + .5f);
        YBounds = new Vector2(minPoint.y - .5f, maxPoint.y + .5f);

        _piecePrefab = piecePrefab;
        _sprites = sprites;
        _audioManager = audioManager;
    }
        
    // Translates an x and y coordinate in the grid to its corresponding world coordinate
    public Vector3 GetWorldPosition(int x, int y)
    {
        return new(position.x - (width - 1) / 2f + x, position.y - (height - 1) / 2f + y);
    }

    // Translates a world coordinate into a corresponding x and y grid coordinate
    public Vector2Int GetGridPosition(Vector2 worldPos)
    {
        return new(Mathf.RoundToInt(worldPos.x + (width - 1) / 2f - position.x), 
                    Mathf.RoundToInt(worldPos.y + (height - 1) / 2f - position.y));
    }

    // Creates a randomly coloured piece at a specified grid coordinate
    public void CreateRandomPiece(int x, int y)
    {
        GameObject piece = Instantiate(_piecePrefab, GetWorldPosition(x, y), Quaternion.identity, transform);

        int colour = Random.Range(0, _sprites.Length);
        
        piece.GetComponent<SpriteRenderer>().sprite = _sprites[colour];
        piece.GetComponent<Piece>().Initialise(this, x, y, colour);

        _pieces[x, y] = piece.GetComponent<Piece>();
    }

    // Creates a piece of a specified colour and type at a specified grid coordinate
    public void CreatePiece(int x, int y, int colour, PieceType type)
    {
        GameObject piece = Instantiate(_piecePrefab, GetWorldPosition(x, y), Quaternion.identity, transform);

        piece.GetComponent<SpriteRenderer>().sprite = _sprites[colour];
        piece.GetComponent<Piece>().Initialise(this, x, y, colour, type);
        
        _pieces[x, y] = piece.GetComponent<Piece>();
    }

    // Removes the piece at a specified grid coordinate, and carries out special actions for certain piece types
    public void RemovePiece(int x, int y, bool recursive = false)
    {
        if (x < 0 || x >= width || y < 0 || y >= height) return;

        Piece piece = _pieces[x, y];

        if (piece == null) return;
        
        _pieces[x, y] = null;

        int score = NormalScore;
        
        if (piece.Type == PieceType.Horizontal)
        {
            _audioManager.activate4.Play();
            
            score = ClearScore;
            
            for (int i = 0; i < width; i++)
            {
                if (i != x) RemovePiece(i, y, true);
            }
        }

        if (piece.Type == PieceType.Vertical)
        {       
            _audioManager.activate4.Play();

            score = ClearScore;
            
            for (int i = 0; i < height; i++)
            {
                if (i != y) RemovePiece(x, i, true);
            }
        }

        if (piece.Type == PieceType.Rainbow) score = RainbowScore;

        if (piece.Type == PieceType.Rainbow && recursive)
        {
            _audioManager.activate5.Play();

            int randomColour = Random.Range(0, _sprites.Length);
            
            for (int i = 0; i < width; i++)
            for (int j = 0; j < height; j++)
            {
                if (this[i, j].Colour == randomColour)
                    RemovePiece(i, j, true);
            }   
        }

        Score += score;
        
        piece.PlayClearAnimation();
        Destroy(piece.gameObject, 1f);
    }

    // Finds all the pieces that currently need to be dropped in the grid, and drops them
    // Also handles creating new pieces to fill the grid
    public bool DropPieces()
    {
        bool pieceMoved = false;
        
        for (int x = 0; x < width; x++)
        {
            for (int y = 1; y < height; y++)
            {
                if (_pieces[x, y - 1] != null || _pieces[x, y] == null) continue;
                
                pieceMoved = true;
                StartCoroutine(DropPiece(_pieces[x, y], y - 1));
            }

            if (_pieces[x, height - 1] != null) continue;
            
            CreateRandomPiece(x, height);
            StartCoroutine(DropPiece(_pieces[x, height], height - 1));
            
            pieceMoved = true;
        }

        return pieceMoved;
    }

    // Drops a specified piece to a specified y coordinate
    private IEnumerator DropPiece(Piece piece, int y)
    {
        _pieces[piece.X, piece.Y] = null;
        _pieces[piece.X, y] = piece;
        
        Vector3 startPos = GetWorldPosition(piece.X, piece.Y);
        Vector3 endPos = GetWorldPosition(piece.X, y);
        
        for (float t = 0; t <= fillTime; t += Time.deltaTime)
        {
            if (piece == null)
                yield break;
            
            piece.transform.position = Vector3.Lerp(startPos, endPos, t / fillTime);
            yield return null;
        }

        if (piece == null) yield break;
        
        piece.transform.position = endPos; 
        piece.Y = y;
    }

    // Moves a piece's object to a specified grid position without changing the grid
    // Used for sliding pieces before actually moving them
    public IEnumerator MovePiece(Piece piece, int x, int y)
    {
        Vector3 startPos = GetWorldPosition(piece.X, piece.Y);
        Vector3 endPos = GetWorldPosition(x, y);

        for (float t = 0; t <= fillTime; t += Time.deltaTime)
        {
            if (piece == null)
                yield break;
            
            piece.transform.position = Vector3.Lerp(startPos, endPos, t / fillTime);
            yield return null;
        }
        
        if (piece != null)
            piece.transform.position = endPos;
    }

    // Resets a piece to the world position of its grid coordinate
    public IEnumerator ResetPiece(Piece piece)
    {
        Vector3 startPos = new(piece.transform.position.x, piece.transform.position.y, 0);
        Vector3 endPos = GetWorldPosition(piece.X, piece.Y);

        for (float t = 0; t <= fillTime; t += Time.deltaTime)
        {
            if (piece == null)
                yield break;
            
            piece.transform.position = Vector3.Lerp(startPos, endPos, t / fillTime);
            yield return null;
        }
        
        if (piece != null)
            piece.transform.position = endPos;
    }

    // Checks if the piece at a specified grid coordinate has any matches
    public List<Piece> GetMatches(int x, int y)
    {
        Piece piece = _pieces[x, y];
        
        List<Piece> matches = new();
        
        if (piece.Colour < 0) return matches;
        
        List<Piece> horizontalMatches = new();

        for (int direction = -1; direction <= 1; direction += 2)
        for (int dx = 1; piece.X + dx * direction >= 0 && piece.X + dx * direction < width; dx++) 
        { 
            if (_pieces[piece.X + dx * direction, piece.Y].Colour == piece.Colour) 
                horizontalMatches.Add(_pieces[piece.X + dx * direction, piece.Y]);
            else break;
        }

        List<Piece> verticalMatches = new();

        for (int direction = -1; direction <= 1; direction += 2)
        for (int dy = 1; piece.Y + dy * direction >= 0 && piece.Y + dy * direction < height; dy++)
        {
            if (_pieces[piece.X, piece.Y + dy * direction].Colour == piece.Colour)
                verticalMatches.Add(_pieces[piece.X, piece.Y + dy * direction]);
            else break;
        }

        if (verticalMatches.Count >= 2) matches.AddRange(verticalMatches);
        if (horizontalMatches.Count >= 2) matches.AddRange(horizontalMatches);
        if (verticalMatches.Count >= 2 || horizontalMatches.Count >= 2) matches.Add(piece);

        return matches;
    }
}