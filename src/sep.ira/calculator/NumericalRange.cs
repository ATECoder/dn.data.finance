namespace cc.isr.Finance.Sep.Ira;

/// <summary>   A numerical range. </summary>
/// <remarks>   2026-06-15. </remarks>
public class NumericalRange
{
    /// <summary>   Gets or sets the minimum. </summary>
    /// <value> The minimum value. </value>
    public decimal Minimum { get; set; }

    /// <summary>   Gets or sets the maximum. </summary>
    /// <value> The maximum value. </value>
    public decimal Maximum { get; set; }

    /// <summary>   Constructor. </summary>
    /// <remarks>   2026-06-15. </remarks>
    /// <exception cref="ArgumentException">    Thrown when one or more arguments have unsupported or
    ///                                         illegal values. </exception>
    /// <param name="minimum">  The minimum value. </param>
    /// <param name="maximum">  The maximum value. </param>
    public NumericalRange( decimal minimum, decimal maximum )
    {
        if ( minimum > maximum )
            throw new ArgumentException( "Minimum cannot be greater than maximum." );
        this.Minimum = minimum;
        this.Maximum = maximum;
    }
    /// <summary>   Determines whether the range contains a specified value. </summary>
    /// <remarks>   2026-06-15. </remarks>
    /// <param name="value">    The value to check. </param>
    /// <returns>   True if the range contains the value; otherwise, false. </returns>
    public bool Contains( decimal value )
    {
        return value >= this.Minimum && value <= this.Maximum;
    }
}
