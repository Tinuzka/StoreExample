''' <summary>
''' This class tells one condtion by which products are filtered.
''' </summary>
Public Class GroupingCondition

    Property minValue As Decimal?

    Property maxValue As Decimal?

    ''' <summary>
    ''' Is the minimum value included in search are only items bigger than it included
    ''' </summary>
    Property minValueIsInclude As Boolean

    ''' <summary>
    ''' Is the maximum value included in search are only items under it included
    ''' </summary>
    Property maxValueIsInclude As Boolean

    Public Sub New(minValue As Decimal?, maxValue As Decimal?, Optional includeMinValue As Boolean = True, Optional includeMaxValue As Boolean = True)
        Me.minValue = minValue
        Me.maxValue = maxValue
        Me.minValueIsInclude = includeMinValue
        Me.maxValueIsInclude = includeMaxValue
    End Sub

End Class
