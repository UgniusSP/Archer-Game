using DefaultNamespace;
using UnityEngine;
using Slider = UnityEngine.UI.Slider;

namespace Weapon
{
    public class Bow : MonoBehaviour, IDieable
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
            
        }

        public void Update()
        {
            
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
            GameObject arrowObject = Instantiate(arrowPrefab, shotPoint.position, shotPoint.rotation);
            Arrow arrow = arrowObject.GetComponent<Arrow>();
            Rigidbody2D arrowRigidbody = arrowObject.GetComponent<Rigidbody2D>();
            arrowRigidbody.velocity = shotPoint.right * _currentCharge;
        
            // Deconstruct arrow and set damage
            var (arrowRigidbody2D, damage) = arrow;
            damage = CalculateDamage(_currentCharge);
            arrow.SetDamage(damage);

            _isCharging = false;
            arrowGfx.enabled = false;
            bowPowerSlider.value = BaseCharge;
        }
    
        private float CalculateDamage(float charge)
        {
            return (charge / MaxCharge) * maxDamage;
        }
        
        public void Die()
        {
            Destroy(bowPowerSlider);
        }
        
    }
}
