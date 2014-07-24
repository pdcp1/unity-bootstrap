using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[ExecuteInEditMode]
public enum EmitterType{
	Point
}

public class ParticleEngine : MonoBehaviour {
	
	public class tk2dParticle{
		private ParticleEffect effect;
		public tk2dSprite Shape;
		public float LifeTime;
		public float StartTime;
		public Vector3 StartPosition;
		public Vector3 Velocity;
		public Vector3 Acceleration;
		public float Speed;
		public Vector3 SpinFactor;
		public Vector3 Scale;
		public float StartScaleFactor;
		public float FinalScaleFactor;
		public float StartAlpha;
		public float FinalAlpha;
		public Color Tint;
		public Color FinalTint;
		
		public tk2dParticle(ParticleEffect effect, tk2dSprite shape){
			this.effect = effect;
			this.Shape = shape;
		}
		
		public void Update(float totalElapsedTime){
			float localElapsedTime = (totalElapsedTime - this.StartTime); 
			float normalizedTime = localElapsedTime / this.LifeTime;
			float clippedNormalizedTime = Mathf.Clamp01(normalizedTime);
			
			if(localElapsedTime > this.LifeTime){
				this.effect.killParticle(this);
				return;
			}
			
			Vector3 position = this.StartPosition + this.Velocity * this.Speed * localElapsedTime + 0.5f * this.Acceleration * localElapsedTime * localElapsedTime;
			this.Shape.transform.position = position;
			this.Shape.transform.Rotate(this.SpinFactor * Time.deltaTime);
			this.Shape.scale = this.Scale * Mathf.Lerp(this.StartScaleFactor, this.FinalScaleFactor, clippedNormalizedTime);
			Color shapeColor = this.Shape.color;
			shapeColor = Color.Lerp(this.Tint, this.FinalTint, clippedNormalizedTime);
			shapeColor.a = Mathf.Clamp(Mathf.Lerp(this.StartAlpha, this.FinalAlpha, clippedNormalizedTime), 0f, 255f);
			this.Shape.color = shapeColor;
		}
	}
	
	[System.Serializable]
	public class ParticleEffect{
		public bool isEnabled;
		//EmiterProperties
		public float EmitStart;
		public float EmitStop;
		public float Rate = 3.0f;
		public bool InheritMovement = false;
		public EmitterType EmitterType;
		
		//ParticleProperties
		public tk2dSprite Shape;
		public float LifeSpan = 30.0f;
		public float LifeSpanVariation = 20.0f;
		
		public Vector3 Direction;
		public Vector3 DirectionVariation;
		public float Speed = 1.0f;
		public float SpeedVariation = 0.0f;
		public Vector3 Acceleration;
		public Vector3 Spin;
		public Vector3 SpinVariation;
		public Vector3 Rotation;
		public Vector3 RotationVariation;
		
		public float Scale = 1.0f;
		public float ScaleVariation = 0.0f;
		public float FinalScale = 1.0f;
		public float FinalScaleVariation;
		
		public float Alpha = 255.0f;
		public float AlphaVariation;
		public float FinalAlpha = 255.0f;
		public float FinalAlphaVariation;
		
		public Color Tint = Color.white;
		public Color FinalTint = Color.white;
		
		
		private List<tk2dParticle> availableParticles;
		private List<tk2dParticle> activeParticles;
		private List<tk2dParticle> particlesToDeactivate;
		
		private float timePerParticleEmision;
		private float timeSinceLastEmision;
		private ParticleEngine engine;
		
		public ParticleEffect(){
			this.EmitStart = 0;
			this.EmitStop = 10;
			this.Rate = 3.0f;
		}
		
		public void Initialize(ParticleEngine engine){
			this.engine = engine;
			float maxParticles = 0.0f;
			if(this.EmitStop == -1)
				maxParticles = (this.LifeSpan + LifeSpanVariation +1) * this.Rate;
			else
				maxParticles = (this.EmitStop - this.EmitStart) * this.Rate;
			
			this.timeSinceLastEmision = 0.0f;
			this.timePerParticleEmision = 1.0f / this.Rate;
			
			this.availableParticles = new List<tk2dParticle>((int)maxParticles);
			this.activeParticles = new List<tk2dParticle>((int)maxParticles);
			this.particlesToDeactivate = new List<tk2dParticle>((int)maxParticles);
			
			tk2dParticle tempParticle = null;
			for(int i=0; i < maxParticles; i++){
				tk2dSprite obj = (tk2dSprite)Instantiate(this.Shape);
				tempParticle = new tk2dParticle(this, obj);
				this.killParticle(tempParticle);
			}
			
			
			
		}
		
		internal void EmitParticle(){
			if(this.availableParticles.Count == 0)
				return;
			tk2dParticle particle = this.availableParticles[0];
			this.availableParticles.RemoveAt(0);
			this.activeParticles.Add(particle);
			
			GameObject obj = particle.Shape.gameObject;
			obj.SetActive(true);
			if(!this.InheritMovement)
				obj.transform.parent = null;
			obj.transform.position = this.engine.selfTransform.position;
			
			particle.LifeTime = Random.Range(this.LifeSpan - this.LifeSpanVariation, this.LifeSpan + this.LifeSpanVariation);
			particle.StartTime = Time.time;
			particle.StartPosition = obj.transform.position;
			particle.Velocity = Quaternion.Euler(
				Random.Range(this.DirectionVariation.x * -1, this.DirectionVariation.x), 
				Random.Range(this.DirectionVariation.y * -1, this.DirectionVariation.y), 
				Random.Range(this.DirectionVariation.z * -1, this.DirectionVariation.z)) * this.Direction;
			particle.Velocity.Normalize();
			particle.Speed = Random.Range(this.Speed - this.SpeedVariation, this.Speed + this.SpeedVariation);
			particle.Acceleration = this.Acceleration;
			particle.Shape.transform.Rotate(
				Random.Range(this.Rotation.x - this.RotationVariation.x, this.Rotation.x + this.RotationVariation.x),
				Random.Range(this.Rotation.y - this.RotationVariation.y, this.Rotation.y + this.RotationVariation.y),
				Random.Range(this.Rotation.z - this.RotationVariation.z, this.Rotation.z + this.RotationVariation.z));
			particle.SpinFactor = new Vector3(
				Random.Range(this.Spin.x - this.SpinVariation.x, this.Spin.x + this.SpinVariation.x), 
				Random.Range(this.Spin.y - this.SpinVariation.y, this.Spin.y + this.SpinVariation.y), 
				Random.Range(this.Spin.z - this.SpinVariation.z, this.Spin.z + this.SpinVariation.z));
			
			particle.Shape.scale = this.Shape.scale;
			particle.Scale = particle.Shape.scale;
			particle.StartScaleFactor = Random.Range(this.Scale - this.ScaleVariation, this.Scale + this.ScaleVariation);
			particle.FinalScaleFactor = Random.Range(this.FinalScale - this.FinalScaleVariation, this.FinalScale + this.FinalScaleVariation);
			
			particle.StartAlpha = Random.Range(this.Alpha - this.FinalAlphaVariation, this.Alpha + this.AlphaVariation);
			particle.FinalAlpha = Random.Range(this.FinalAlpha - this.FinalAlphaVariation, this.FinalAlpha + this.FinalAlphaVariation);
			
			particle.Tint = this.Tint;
			particle.FinalTint = this.FinalTint;
		}
		
		internal void killParticle(tk2dParticle particle){
			this.particlesToDeactivate.Add(particle);
		}
		
		private void deactivateParticle(tk2dParticle particle){
			GameObject obj = particle.Shape.gameObject;
			obj.SetActive(false);
			obj.transform.parent = this.engine.selfTransform;
			this.activeParticles.Remove(particle);
			this.availableParticles.Add(particle);
		}
		
		internal void Update (float elapsedTime) {
			if(this.engine.isRunning){
				bool isEmitting = false;
				if(this.EmitStop == -1)
					isEmitting = elapsedTime >= this.EmitStart;
				else
					isEmitting = elapsedTime >= this.EmitStart && elapsedTime <= EmitStop;
				if(isEmitting){
					this.timeSinceLastEmision += Time.deltaTime;
					if(this.timeSinceLastEmision >= this.timePerParticleEmision){
						float numOfEmisions = this.timeSinceLastEmision / this.timePerParticleEmision;
						numOfEmisions = Mathf.CeilToInt(numOfEmisions);
						for(int i=0; i< numOfEmisions; i++)
							this.EmitParticle();
						this.timeSinceLastEmision = 0.0f;
					}
				}
			}
			if(this.activeParticles.Count>0){
				foreach(tk2dParticle particle in this.activeParticles)
					particle.Update(elapsedTime);
			}
			if(this.particlesToDeactivate.Count >0){
				foreach(tk2dParticle particle in this.particlesToDeactivate)
					this.deactivateParticle(particle);
				this.particlesToDeactivate.Clear();
			}
		}
	}
	
	
	public bool AutoStartEngine = true;
	public ParticleEffect[] Effects;
	
	internal Transform selfTransform;
	internal bool isRunning;
	private float startTime;
	
	public void StartEngine(){
		this.startTime = Time.time;
		this.isRunning = true;	
	}
	
	public void StopEngine(){
		this.isRunning = false;
	}
	
	// Use this for initialization
	void Start () {
		this.selfTransform = this.gameObject.transform;
		if(Effects == null)
			return;
		for(int i=0 ; i< this.Effects.Length; i++)
			if(this.Effects[i].isEnabled)
				this.Effects[i].Initialize(this);
	}
	
	void Awake(){
		if(this.AutoStartEngine)
			this.StartEngine();
	}
	
	// Update is called once per frame
	void Update () {
		if(Effects == null)
			return;
		float elapsedTime = (Time.time - this.startTime);
		for(int i=0 ; i< this.Effects.Length; i++)
			if(this.Effects[i].isEnabled)
				this.Effects[i].Update(elapsedTime);
	}
}
