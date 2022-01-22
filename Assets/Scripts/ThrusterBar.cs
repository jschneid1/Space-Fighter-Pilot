using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class ThrusterBar : MonoBehaviour
{
    private Player _player;

    [SerializeField]
    private Slider _thrusterBarSlider;

    [SerializeField]
    private float _maxThruster, _thruster, _thrusterFillRate = 0.5f;

    [SerializeField]
    private bool _thrusterBarActive = true;

    [SerializeField]
    private Text _thrusterChargingText;

    [SerializeField]
    private Color _thrusterBarColor;

    [SerializeField]
    private GameObject _thrusterBarInner;

    // Start is called before the first frame update
    void Start()
    {
        _thrusterBarSlider = GetComponent<Slider>();
        _thruster = _maxThruster;
        _thrusterBarSlider.maxValue = _maxThruster;
        _player = GameObject.Find("Player").GetComponent<Player>();
        _thrusterChargingText.enabled = false;

        if (_thrusterBarSlider == null)
        {
            Debug.LogError("There is no Thruster Bar component attached");
        }
    }

    // Update is called once per frame
    void Update()
    {
        Thruster();
    }

    private void ThrusterBarActive()
    {
        if (Input.GetKey(KeyCode.LeftShift) && _thrusterBarActive == true)
        {
            _thruster -= Time.deltaTime;
            _thrusterBarSlider.value = _thruster;
        }
    }

    private void ThrusterBarInActive()
    {
        _thrusterBarActive = false;
        _player.ThrusterDeactivate();
        _thrusterChargingText.enabled = true;
    }

    private void ThrusterFill()
    {
        if (Input.GetKey(KeyCode.LeftShift) == false)
        {
            _thruster += Time.deltaTime * _thrusterFillRate;
            _thrusterBarSlider.value = _thruster;
        }
    }

    private void Thruster()
    {
        if (_thrusterBarActive == true)
        {
            ThrusterBarActive();
        }
        else
        {
            ThrusterFill();
        }

        if (Input.GetKey(KeyCode.LeftShift) == false && _thruster < 5)
        {
            ThrusterFill();
        }

        if (_thruster < 0)
        {
            ThrusterBarInActive();
        }

        else if (_thruster >= 5)
        {
            _thrusterBarActive = true;
            _player.ThrusterAcivate();
            _thrusterChargingText.enabled = false;
        }

        if (_player == null)
        {
            _thrusterBarSlider.enabled = false;
        }
    }
}
