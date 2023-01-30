using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PieceType
{
    Default,
    Horizontal,
    Vertical,
    Rainbow
}

public class Piece : MonoBehaviour
{
    // Piece settings
    public Sprite rainbowSprite;   
    public float shakeAmount = .06f;
    public float shakeSpeed = 2.5f;

    private Grid _grid;
    private Animator _animator;
    private bool _positiveMovement = true;
    
    // Properties
    public int X { get; set; }
    public int Y { get; set; }
    public int Colour { get; private set; }
    public PieceType Type { get; set; }

    // Plays the piece's clear animation
    public void PlayClearAnimation()
    {
        _animator.SetBool("Cleared", true);
    }
    
    // Initialises a new piece with a set of given attributes
    public void Initialise(Grid grid, int x, int y, int colour, PieceType type = PieceType.Default)
    {
        _grid = grid;
        _animator = GetComponent<Animator>();

        X = x;
        Y = y;
        Colour = colour;
        Type = type;

        if (type != PieceType.Rainbow) return;
        
        Colour = -1;
        GetComponent<SpriteRenderer>().sprite = rainbowSprite;
    }
    // Shakes Horizontal and Vertical pieces
    private void Update()
    {
        if (Type != PieceType.Horizontal && Type != PieceType.Vertical) return;
        
        Vector3 differenceVector = transform.position - _grid.GetWorldPosition(X, Y); 
        float difference = Type == PieceType.Horizontal ? differenceVector.x : differenceVector.y;
        
        Vector3 positiveVector = Type == PieceType.Horizontal ? Vector3.right : Vector3.up;
        
        if (_positiveMovement && difference >= shakeAmount) 
            _positiveMovement = false;

        if (!_positiveMovement && difference <= -shakeAmount) 
            _positiveMovement = true;
            
        if (_positiveMovement && difference < shakeAmount) 
            transform.position += Time.deltaTime * shakeSpeed * positiveVector;

        if (!_positiveMovement && difference > -shakeAmount) 
            transform.position += -Time.deltaTime * shakeSpeed * positiveVector;
    }
}
