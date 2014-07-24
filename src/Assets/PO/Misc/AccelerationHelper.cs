// Version 1.04
// Changed Line 65 to make this script Unity3 pre-release compatible
// Version 1.03
// Implemented LowPassFilterAdaptive and HighFilterAdaptive filter function.
// Updated the whole class with minor improvements. 
// Version 1.02
// Lowered the start values of accToleranceXAxis01 and accToleranceXAxis02 to prevent ruff 
// tilting behavior at the initial game play. 

using UnityEngine;
using System.Collections;


public static class AccelerationHelper
{

    /// <summary>
    /// Use this function for default class initialization.
    /// </summary>
    static AccelerationHelper()
    {
        acceleration = Vector3.zero;
        lastAcceleration = Vector3.zero;
    }

    /// <summary>
    /// Store the current acceleration.
    /// </summary>
    private static Vector3 acceleration;

    /// <summary>
    /// Provide the Current Acceleration.
    /// </summary>
    /// <returns>Vector3</returns>
    public static Vector3 Acceleration
    {
        get { return acceleration; }
    }

    /// <summary>
    /// Store the acceleration before the current acceleration.
    /// </summary>
    private static Vector3 lastAcceleration;

    /// <summary>
    /// Provide the last acceleration before the current acceleration.
    /// </summary>
    /// <returns>Vector3</returns>
    public static Vector3 LastAcceleration
    {
        get { return lastAcceleration; }
    }

    /// <summary>
    /// OnUpdate is called from an other Mono behavior class. Use inside that
    /// class the Update or FixedUpdate function to call OnUpdate. This will assure that 
    /// everything will be synchronized.
    /// </summary>
    public static void OnUpdate()
    {
        // Write the current acceleration to the lastAcceleration.
        lastAcceleration = acceleration;

        //Correct the acceleration if there is a match with the two lowest deviations.
        acceleration = CorrectRawAcceleration(lastAcceleration, Input.acceleration);
    }

    // Internal helper function.
    private static float Norm(float x, float y, float z)
    {
        return Mathf.Sqrt(x * x + y * y + z * z);
    }

    /// <summary>
    /// Isolating the effects of gravity from accelerometer data with a lowPassFilter.
    /// LowPassFilteringFactor needs to be between 0.01 and 0.99 float.
    /// </summary>
    /// <param name="lastAcc">Last Acceleration</param>
    /// <param name="currentAcc">Current Acceleration</param>
    /// <param name="lowPassFilteringFactor">Low Pass Filtering Factor</param>
    /// <returns>Vector3</returns>
    public static Vector3 LowPassFilter(Vector3 lastAcc, Vector3 currentAcc, float lowPassFilteringFactor)
    {
        currentAcc.x = (currentAcc.x * lowPassFilteringFactor) + (lastAcc.x * (1.0f - lowPassFilteringFactor));
        currentAcc.y = (currentAcc.y * lowPassFilteringFactor) + (lastAcc.y * (1.0f - lowPassFilteringFactor));
        currentAcc.z = (currentAcc.z * lowPassFilteringFactor) + (lastAcc.z * (1.0f - lowPassFilteringFactor));

        return currentAcc;
    }

    /// <summary>
    /// Isolating the effects of gravity from accelerometer data with an adaptive lowPassFilter.
    /// </summary>
    /// <param name="lastAcc">Last Acceleration</param>
    /// <param name="currentAcc">Current Acceleration</param>
    /// <param name="frequency">Default 60.0f</param>
    /// <param name="cutoffFrequency">Default 5.0f</param>
    /// <param name="adaptive">true or false</param>
    /// <returns>Vector3</returns>
    public static Vector3 LowPassFilterAdaptive(Vector3 lastAcc, Vector3 currentAcc, float frequency, float cutoffFrequency, bool adaptive)
    {
        float dt = 1.0f / frequency;
        float RC = 1.0f / cutoffFrequency;

        float filterConstant = dt / (dt + RC);

        float alpha = filterConstant;

        if (adaptive)
        {
            float kAccelerometerMinStep = 0.02f;
            float kAccelerometerNoiseAttenuation = 3.0f;

            float d = Mathf.Clamp(Mathf.Abs(Norm(lastAcc.x, lastAcc.y, lastAcc.z) - Norm(currentAcc.x, currentAcc.y, currentAcc.z)) / kAccelerometerMinStep - 1.0f, 0.0f, 1.0f);
            alpha = (1.0f - d) * filterConstant / kAccelerometerNoiseAttenuation + d * filterConstant;
        }

        currentAcc.x = currentAcc.x * alpha + lastAcc.x * (1.0f - alpha);
        currentAcc.y = currentAcc.y * alpha + lastAcc.y * (1.0f - alpha);
        currentAcc.z = currentAcc.z * alpha + lastAcc.z * (1.0f - alpha);

        return currentAcc;
    }

    /// <summary>
    /// Isolating Instantaneous Motion from Acceleration Data.
    /// highPassFilteringfactor needs to be between 0.01 and 0.99 float
    /// </summary>
    /// <param name="lastAcc">Last Acceleration</param>
    /// <param name="currentAcc">Current Acceleration</param>
    /// <param name="highPassFilteringFactor">High Pass Filtering Factor</param>
    /// <returns>Vector3</returns>
    public static Vector3 HighPassFilter(Vector3 lastAcc, Vector3 currentAcc, float highPassFilteringsFactor)
    {
        // Subtract the low-pass value from the current value to get a simplified high-pass filter
        currentAcc.x = lastAcc.x - ((lastAcc.x * highPassFilteringsFactor) + (currentAcc.x * (1.0f - highPassFilteringsFactor)));
        currentAcc.y = lastAcc.y - ((lastAcc.y * highPassFilteringsFactor) + (currentAcc.y * (1.0f - highPassFilteringsFactor)));
        currentAcc.z = lastAcc.z - ((lastAcc.z * highPassFilteringsFactor) + (currentAcc.z * (1.0f - highPassFilteringsFactor)));

        return currentAcc;
    }

    /// <summary>
    /// Isolating Instantaneous Motion from Acceleration Data with an adaptive HighPassFilter.
    /// </summary>
    /// <param name="lastAcc">Last Acceleration</param>
    /// <param name="currentAcc">Current Acceleration</param>
    /// <param name="frequency">Default 60.0f</param>
    /// <param name="cutoffFrequency">Default 5.0f</param>
    /// <param name="adaptive">true or false</param>
    /// <returns>Vector3</returns>
    public static Vector3 HighPassFilterAdaptive(Vector3 lastAcc, Vector3 currentAcc, float frequency, float cutoffFrequency, bool adaptive)
    {
        float dt = 1.0f / frequency;
        float RC = 1.0f / cutoffFrequency;

        float filterConstant = dt / (dt + RC);

        float alpha = filterConstant;

        if (adaptive)
        {
            float kAccelerometerMinStep = 0.02f;
            float kAccelerometerNoiseAttenuation = 3.0f;

            float d = Mathf.Clamp(Mathf.Abs(Norm(lastAcc.x, lastAcc.y, lastAcc.z) - Norm(currentAcc.x, currentAcc.y, currentAcc.z)) / kAccelerometerMinStep - 1.0f, 0.0f, 1.0f);
            alpha = d * filterConstant / kAccelerometerNoiseAttenuation + (1.0f - d) * filterConstant;
        }

        currentAcc.x = alpha * (currentAcc.x - lastAcc.x);
        currentAcc.y = alpha * (currentAcc.y - lastAcc.y);
        currentAcc.z = alpha * (currentAcc.z - lastAcc.z);


        return currentAcc;
    }


    /// <summary>
    /// Private class variables to keep track of the lowest two deviations. 
    /// </summary>
    private static float accToleranceXAxis01 = 0.03f;
    private static float accToleranceYAxis01 = 0.03f;
    private static float accToleranceZAxis01 = 0.03f;

    private static float accToleranceXAxis02 = 0.06f;
    private static float accToleranceYAxis02 = 0.06f;
    private static float accToleranceZAxis02 = 0.06f;

    /// <summary>
    /// Subtract the current acceleration from the last acceleration and match the 
    /// difference with the two lowest Acceleration Deviations. Correct the 
    /// acceleration if there is a match.
    /// </summary>
    /// <param name="lastAcc">Last Acceleration</param>
    /// <param name="currentAcc">Current Acceleration</param>
    private static Vector3 CorrectRawAcceleration(Vector3 lastAcc, Vector3 currentAcc)
    {
        float diffX = Mathf.Abs(currentAcc.x - lastAcc.x);
        float diffY = Mathf.Abs(currentAcc.y - lastAcc.y);
        float diffZ = Mathf.Abs(currentAcc.z - lastAcc.z);

        // Find the first Positive lowest Acceleration Deviations for the X axis.
        accToleranceXAxis01 = (diffX > 0 && diffX <= accToleranceXAxis01) ? diffX : accToleranceXAxis01;
        // Find the second Positive lowest Acceleration Deviations for the X axis.
        accToleranceXAxis02 = (diffX > accToleranceXAxis01 && diffX <= accToleranceXAxis02) ? diffX : accToleranceXAxis02;

        // Find the first Positive lowest Acceleration Deviations for the Y axis.
        accToleranceYAxis01 = (diffY > 0 && diffY <= accToleranceYAxis01) ? diffY : accToleranceYAxis01;
        // Find the second Positive lowest Acceleration Deviations for the Y axis.
        accToleranceYAxis02 = (diffY > accToleranceYAxis01 && diffY <= accToleranceYAxis02) ? diffY : accToleranceYAxis02;

        // Find the first Positive lowest Acceleration Deviations for the Z axis.
        accToleranceZAxis01 = (diffZ > 0 && diffZ <= accToleranceZAxis01) ? diffZ : accToleranceZAxis01;
        // Find the second Positive lowest Acceleration Deviations for the Z axis.
        accToleranceZAxis02 = (diffZ > accToleranceZAxis01 && diffZ <= accToleranceZAxis02) ? diffZ : accToleranceZAxis02;

        //Correct the X axis if necessary.
        if (accToleranceXAxis01 == diffX)
        {
            currentAcc.x = (currentAcc.x > 0) ? currentAcc.x -= accToleranceXAxis01 : currentAcc.x += accToleranceXAxis01;
        }

        if (accToleranceXAxis02 == diffX)
        {
            currentAcc.x = (currentAcc.x > 0) ? currentAcc.x -= accToleranceXAxis02 : currentAcc.x += accToleranceXAxis02;
        }

        //Correct the Y axis if necessary.
        if (accToleranceYAxis01 == diffY)
        {
            currentAcc.y = (currentAcc.y > 0) ? currentAcc.y -= accToleranceYAxis01 : currentAcc.y += accToleranceYAxis01;
        }

        if (accToleranceYAxis02 == diffY)
        {
            currentAcc.y = (currentAcc.y > 0) ? currentAcc.y -= accToleranceYAxis02 : currentAcc.y += accToleranceYAxis02;
        }

        //Correct the Z axis if necessary.
        if (accToleranceZAxis01 == diffZ)
        {
            currentAcc.z = (currentAcc.z > 0) ? currentAcc.z -= accToleranceZAxis01 : currentAcc.z += accToleranceZAxis01;
        }

        if (accToleranceZAxis02 == diffZ)
        {
            currentAcc.z = (currentAcc.z > 0) ? currentAcc.z -= accToleranceZAxis02 : currentAcc.z += accToleranceZAxis02;
        }

        return currentAcc;
    }
}