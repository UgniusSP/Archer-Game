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
            try
            {
                InitializeBow();
            }
            catch (System.Exception ex)
            {
                Debug.LogError($"Error initializing bow: {ex.Message}");
            }
        }

        private void InitializeBow()
        {
            bowPowerSlider.value = 0;
            bowPowerSlider.maxValue = MaxCharge;
        }

        public void Update()
        {
            try
            {
                HandleInput();
            }
            catch (System.Exception ex)
            {
                Debug.LogError($"Error in update: {ex.Message}");
            }
        }

        private void HandleInput()
        {
            if (Input.GetMouseButtonDown(0))
            {
                StartCharging();
            }
        
            if (Input.GetMouseButton(0) && _isCharging)
            {
                ChargeBow();
            }
            else if (Input.GetMouseButtonUp(0) && _isCharging)
            {
                StopChargingAndShoot();
            }
        }

        private void StartCharging()
        {
            try
            {
                _isCharging = true;
                _currentCharge = BaseCharge;
                arrowGfx.enabled = true;
            }
            catch (System.Exception ex)
            {
                Debug.LogError($"Error starting charge: {ex.Message}");
            }
        }

        private void ChargeBow()
        {
            try
            {
                arrowGfx.enabled = true;
                _currentCharge += ChargeRate * Time.deltaTime;
                _currentCharge = Mathf.Clamp(_currentCharge, BaseCharge, MaxCharge);
                bowPowerSlider.value = _currentCharge;
            }
            catch (System.Exception ex)
            {
                Debug.LogError($"Error charging bow: {ex.Message}");
            }
        }

        private void StopChargingAndShoot()
        {
            try
            {
                _isCharging = false;
                Shoot();
            }
            catch (System.Exception ex)
            {
                Debug.LogError($"Error shooting: {ex.Message}");
            }
        }

        private void Shoot()
        {
            try
            {
                GameObject arrowObject = Instantiate(arrowPrefab, shotPoint.position, shotPoint.rotation);
                Arrow arrow = arrowObject.GetComponent<Arrow>();
                Rigidbody2D arrowRigidbody = arrowObject.GetComponent<Rigidbody2D>();
                arrowRigidbody.velocity = shotPoint.right * _currentCharge;

                // Deconstruct arrow and set damage
                var (arrowRigidbody2D, damage) = arrow;
                damage = CalculateDamage(_currentCharge);
                arrow.SetDamage(damage);

                ResetBowState();
            }
            catch (System.Exception ex)
            {
                Debug.LogError($"Error during shoot: {ex.Message}");
            }
        }

        private float CalculateDamage(float charge)
        {
            try
            {
                return (charge / MaxCharge) * maxDamage;
            }
            catch (System.Exception ex)
            {
                Debug.LogError($"Error calculating damage: {ex.Message}");
                return 0f; 
            }
        }

        private void ResetBowState()
        {
            _isCharging = false;
            arrowGfx.enabled = false;
            bowPowerSlider.value = BaseCharge;
        }

        public void Die()
        {
            try
            {
                DestroyBow();
            }
            catch (System.Exception ex)
            {
                Debug.LogError($"Error destroying bow: {ex.Message}");
            }
        }

        private void DestroyBow()
        {
            try
            {
                Destroy(bowPowerSlider.gameObject);
            }
            catch (System.Exception ex)
            {
                Debug.LogError($"Error destroying bow power slider: {ex.Message}");
            }
        }
    }
}
