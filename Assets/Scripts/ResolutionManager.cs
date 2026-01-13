using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ResolutionManager : MonoBehaviour
{
    [SerializeField] private TMP_Dropdown _resolutionDropdown;

    private Resolution[] _allResolutions;
    private List<Resolution> _filteredResolutions;

    private float _currentRefreshRate;
    private int _currentResolutionIndex = 0;

    private void Start()
    {
        // Get all possible resolutions
        _allResolutions = Screen.resolutions;

        // Create a list of filtered resolutions based on the current refresh rate of the screen
        _filteredResolutions = new List<Resolution>();
        _currentRefreshRate = (float)Screen.currentResolution.refreshRateRatio.value;

        for (int i = 0; i < _allResolutions.Length; i++)
        {
            if (Mathf.Approximately((float)_allResolutions[i].refreshRateRatio.value, _currentRefreshRate))
            {
                _filteredResolutions.Add(_allResolutions[i]);
            }
        }

        // create Options for the Dropdown
        List<string> options = new List<string>();
        for (int i = 0; i < _filteredResolutions.Count; i++)
        {
            string resOption = $"{_filteredResolutions[i].width}x{_filteredResolutions[i].height} {_filteredResolutions[i].refreshRateRatio.value} Hz";
            options.Add(resOption);

            if (_filteredResolutions[i].width == Screen.width && _filteredResolutions[i].height == Screen.height) _currentResolutionIndex = i;
        }

        _resolutionDropdown.ClearOptions();
        _resolutionDropdown.AddOptions(options);

        _resolutionDropdown.value = _currentResolutionIndex;
        _resolutionDropdown.RefreshShownValue();
    }

    public void SetResolution(int resolutionIndex)
    {
        Resolution res = _filteredResolutions[resolutionIndex];
        Screen.SetResolution(res.width, res.height, true);
    }
}
