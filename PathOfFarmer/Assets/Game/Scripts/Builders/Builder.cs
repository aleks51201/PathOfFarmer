using UnityEngine;

namespace Assets.Game.Scripts.Builders
{
    public class Builder
    {
        private readonly BuildObject _buildObject;

        public Builder(BuildObfectView buildObfectViewPrefab, Transform parentTransrorm)
        {
            _buildObject = new BuildObject(buildObfectViewPrefab, parentTransrorm);
        }
        public bool IsBuilding { get; private set; }

        public void Start()
        {
            IsBuilding = true;

            _buildObject.Spawn();
        }

        public void Stop()
        {
            IsBuilding = false;
        }

        public void Tick()
        {
            if (!IsBuilding) return;

            if (TryCastRay(out Vector3 point))
            {
                Move(point);
            }
        }

        public void Build()
        {
            Stop();

            _buildObject.Complete();
        }

        private void Move(Vector3 position)
        {
            _buildObject.View.transform.position = position;
        }

        public void Rotate()
        {
            _buildObject.View.transform.Rotate(_buildObject.View.transform.rotation.x, _buildObject.View.transform.rotation.y + 90, _buildObject.View.transform.rotation.z);
        }

        private bool TryCastRay(out Vector3 point)
        {
            point = Vector3.zero;

            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity, ~(1 << 6)))
            {
                Debug.Log($"TryCastRay {hit.transform.name}");
                if (hit.transform.CompareTag("Plane"))
                {
                    point = hit.point;
                    return true;
                }
            }

            return false;
        }
    }

    public interface ITick
    {
        void Tick();
    }
}

