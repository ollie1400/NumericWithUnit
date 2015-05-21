A project made in Microsoft Visual Studio Professional 2013.

The built control accepts numeric input (0-9, ".", ",") as well as a unit.

The control has property `AllowedUnits` which is a collection of `NumericUnit.Unit` objects.
Each of these describes a unit and corresponding value.
This list must be build before the control is used.
The control also has `Maximum` and `Minimum` properties which are the maximum and minimum allowed values (with NO unit), which must be set.


# Inclusion

To use this as is, either build the project as is then use the DLL made to add to the Toolbox in another project,
or include the code directly into another project.

To do the former, the full project path MUST NOT INCLUDE THE CHARACTER "#" due to some weird Visual Studio thing.

# Example usage

```
NumericWithUnit nwu = new NumericWithUnit();

// add the units
nwu.AllowedUnits.Add(new NumericWithUnit.Unit("s", 1));            
nwu.AllowedUnits.Add(new NumericWithUnit.Unit("ms", 1e-3));

// set maximum and minimum
nwu.Maximum = 1000;
nwu.Minimum = 1e-3;

// set default value (this automatically updates the text, based on the units currently set in the control,
// e.g. 0.15 would be converted to "150 ms" since "ms" is the first unit to be smaller than 0.15 (since 1e-3 < 0.15))
nwu.Value = 0.15;
```

# User Interaction


now the object nwu is an instance of `NumericWithUnit`.

The user can type in values.
If the value is in the correct format such as "100.4 ms", then the number and unit are extracted, and this is converted to a dimensionless number.
In this case "ms" corresponds to 1e-3.
"100 ms" then is converted to 100 * 1e-3 = 0.1
This is within the maximum and minimum (1e-3 >= 0.1 >= 100) so the background colour of the control is set to `CorrectColor`.

Now, if the user presses Enter, or `VerifyInput()` is called the internal value of the control is updated to 0.1, and the `ValueChanged` event is fired.  If the Enter key was pressed, the `EnterPressed` event is also fired (after).

If the input is either in the wrong format (e.g. "1.5. f4" or something like this which is clearly wrong), or outside the allowed bounds, the background colour is set to `IncorrectColor`.
