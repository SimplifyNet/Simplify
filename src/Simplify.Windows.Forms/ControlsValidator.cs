using System;
using System.Windows.Forms;

namespace Simplify.Windows.Forms;

/// <summary>
/// Validates List of controls items for filled value or existing value
/// </summary>
/// <remarks>
/// Initialize controls validator
/// </remarks>
/// <param name="resultStatusControl">Control which will be disabled or enabled after validation</param>
/// <param name="checkItems">Items to validate</param>
public class ControlsValidator(Control resultStatusControl, params Control[] checkItems)
{
	private bool _validationEnabled;

	/// <summary>
	/// Enable items validation
	/// </summary>
	public void EnableValidation()
	{
		_validationEnabled = true;

		foreach (var item in checkItems)
		{
			var castItemComboBox = item as ComboBox;

			// Special validation for ComboBox controls
			if (castItemComboBox != null)
			{
				if (castItemComboBox.Items.Count == 0 && castItemComboBox.DropDownStyle == ComboBoxStyle.DropDownList)
					castItemComboBox.Enabled = false;
				else
					castItemComboBox.SelectedIndexChanged += OnItemCheckEvent;
			}
			else
				item.TextChanged += OnItemCheckEvent;
		}

		ValidateItems();
	}

	private void ValidateItems()
	{
		foreach (var item in checkItems)
		{
			var castItemComboBox = item as ComboBox;

			if (castItemComboBox != null)
			{
				if (castItemComboBox.DropDownStyle == ComboBoxStyle.DropDownList && castItemComboBox.SelectedIndex == -1)
				{
					resultStatusControl.Enabled = false;
					return;
				}
			}
			else if (item.Text.Length == 0)
			{
				resultStatusControl.Enabled = false;
				return;
			}
		}

		resultStatusControl.Enabled = true;
	}

	private void OnItemCheckEvent(object sender, EventArgs e)
	{
		if (_validationEnabled)
			ValidateItems();
	}
}