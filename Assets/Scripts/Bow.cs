using System.Diagnostics.CodeAnalysis;
using UnityEngine;
using Slider = UnityEngine.UI.Slider;

[SuppressMessage("ReSharper", "Unity.PerformanceCriticalCodeInvocation")]
public class Bow : MonoBehaviour
{
    [SerializeField] private GameObject arrowPrefab;
    [SerializeField] private SpriteRenderer arrowGfx;
    [SerializeField] private Slider bowPowerSlider;
    [SerializeField] private Transform shotPoint;
    [SerializeField] private Vector3 sliderOffset;
    [SerializeField] private float maxDamage = 50f;
    
    private const float ChargeRate = 100f;
    private const float BaseCharge = 0f;
    private const float MaxCharge = 70f;
    
    private bool _isCharging;
    private float _currentCharge;


    public void Start()
    {
        bowPowerSlider.value = 0;
        bowPowerSlider.maxValue = MaxCharge;
        
        sliderOffset = new Vector3(-0.83f, 4.1f, 0);
    }

    public void Update()
    {
        
        bowPowerSlider.transform.position = Camera.main.WorldToScreenPoint(transform.position + sliderOffset);
        
        if (Input.GetMouseButtonDown(0))
        {
            _isCharging = true;
            _currentCharge = BaseCharge;
            arrowGfx.enabled = true;
        }
        
        if(Input.GetMouseButton(0) && _isCharging)
        {
            ChargeBow();
        } else if (Input.GetMouseButtonUp(0) && _isCharging)
        {
            _isCharging = false;
            Shoot();
        }
    }

    private void ChargeBow()
    {
        arrowGfx.enabled = true;
        _currentCharge += ChargeRate * Time.deltaTime;
        _currentCharge = Mathf.Clamp(_currentCharge, BaseCharge, MaxCharge);
        bowPowerSlider.value = _currentCharge;
        
    }

    private void Shoot()
    {
        GameObject arrow = Instantiate(arrowPrefab, shotPoint.position, shotPoint.rotation);
        Rigidbody2D arrowRigidbody = arrow.GetComponent<Rigidbody2D>();
        arrowRigidbody.velocity = shotPoint.right * _currentCharge;
        
        // Calculate damage based on charge
        float damage = CalculateDamage(_currentCharge);
        arrow.GetComponent<Arrow>().SetDamage(damage);

        _isCharging = false;
        arrowGfx.enabled = false;
        bowPowerSlider.value = BaseCharge;
    }
    
    private float CalculateDamage(float charge)
    {
        return (charge / MaxCharge) * maxDamage;
    }

   
}
