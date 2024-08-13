﻿using System.ComponentModel.DataAnnotations;
using NetTopologySuite.Geometries;

public class Location
{
    public int Id { get; set; }
    [Required]
    public string Name { get; set; }
    [Required]
    public string Address { get; set; }
    public Point Coordinates { get; set; }

}
