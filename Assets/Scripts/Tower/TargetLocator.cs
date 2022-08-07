using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetLocator : MonoBehaviour
{
    [SerializeField] Transform weapon;
    [SerializeField] float range = 15f;
    [SerializeField] ParticleSystem projectileParticles;
    [SerializeField] float rotationSpeed = 10f;
    [SerializeField] float atackSpeed = 1f;
    Enemy enemyTarget;

    void Update()
    {
        FindClosestTarget();
        AimWeapon();
    }

    void FindClosestTarget()
    {
        Enemy[] enemies = FindObjectsOfType<Enemy>();
        Enemy closestTarget = null;
        float maxDistance = Mathf.Infinity;

        foreach (Enemy enemy in enemies)
        {
            float targetDistance = Vector3.Distance(transform.position, enemy.transform.position);

            if (targetDistance < maxDistance)
            {
                closestTarget = enemy;
                maxDistance = targetDistance;
            }
        }
        enemyTarget = closestTarget;
    }
    void AimWeapon()
    {
        Vector3 enemyPosition = enemyTarget.transform.position;

        Vector3 enemyMoveDir = (enemyTarget.enemyMover.endPosition - enemyPosition).normalized / 2;
        Vector3 enemyTargetPosition = enemyPosition + enemyMoveDir;

        Vector3 turretRotation = enemyTargetPosition - weapon.transform.position;

        Quaternion dirRotation = Quaternion.LookRotation(turretRotation);
        weapon.transform.rotation = Quaternion.Lerp(weapon.transform.rotation, dirRotation, Time.deltaTime * rotationSpeed * InGameManager.Instance.gameSpeed);

        float targetDistance = Vector3.Distance(transform.position, enemyPosition);

        if (targetDistance < range && Mathf.Abs(Quaternion.Angle(weapon.transform.rotation, dirRotation)) < 10f)
        {
            Attack(true);
        }
        else { Attack(false); }
    }

    void Attack(bool isActive)
    {
        var emissionModule = projectileParticles.emission;
        emissionModule.enabled = isActive;

        emissionModule.rateOverTime = atackSpeed * InGameManager.Instance.gameSpeed;
    }
}
