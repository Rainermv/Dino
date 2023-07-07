using System;
using System.Collections;
using Assets.Scripts.Model;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Assets.Scripts.Controllers
{
    public class PlatformBuilder  
    {
        public PlatformGenerationStrategy GenerationStrategy
        {
            get => _generationStrategy;
            set
            {
                OnChangeStrategy( _generationStrategy, value);
                _generationStrategy = value;
            }
        }

        

        private readonly World _world;
        private readonly ActorFactory _actorFactory;

        FloorPlatformType lastType = FloorPlatformType.CENTER;
        private PlatformGenerationStrategy _generationStrategy;

        private ActorComponent _lastGroundPlatform;
        private ActorComponent _lastAirPlatform;

        private FloorPlatformType _groundType = FloorPlatformType.CENTER;

        public PlatformBuilder(PlatformGenerationStrategy generationStrategy, World world, ActorFactory actorFactory)
        {
            _generationStrategy = generationStrategy;
            _world = world;
            _actorFactory = actorFactory;

        }


        public void CreateInitialPlatforms()
        {
            float min = _world.ScreenModel.ScreenLeft;
            float max = _world.XSpawn;
            float size = _world.FloorPlatformSize;

            //objFactory.buildGroundPlatform(FloorPlatformType.LEFT, min - size);


            for (var pos = min - size; pos < max + size; pos += size)
            {
                _lastGroundPlatform = BuildGroundPlatform(FloorPlatformType.CENTER, pos);
            }
        }

        private void SpawnActor(ActorComponent platform, ActorComponent actor, float translateX)
        {

            //float platformSizeY = (platform.getColliders()[0] as BoxCollider2D).size.y;
            float platformSizeY = platform.getRendererBounds().size.y;

            BoxCollider2D collider = actor.getColliders()[0] as BoxCollider2D;
            float sizeY = collider.size.y;

            actor.transform.position = platform.transform.position;
            actor.transform.Translate(
                translateX,
                platformSizeY * 0.5f + sizeY * 0.5f - collider.offset.y,
                0);

        }

        private void SpawnProp(ActorComponent platform, ActorComponent prop)
        {

            float platformSizeY = platform.getRendererBounds().size.y;
            float propSizeY = prop.getRendererBounds().size.y;

            prop.transform.position = platform.transform.position;
            prop.transform.Translate(0, platformSizeY * 0.5f + propSizeY * 0.5f, 0);

        }

        public IEnumerator EvaluateBuilding()
        {
            while (true)
            {
                Debug.Log(GenerationStrategy);
                switch (GenerationStrategy)
                {
                    case PlatformGenerationStrategy.GroundOnly:
                    {
                        yield return DecideBuildGround();
                        break;
                    }
                    case PlatformGenerationStrategy.AirOnly:
                    {
                        yield return DecideBuildAir();
                        break;
                    }
                    case PlatformGenerationStrategy.Nothing:
                    {
                        break;
                    }
                    default:
                        throw new ArgumentOutOfRangeException();
                }

                yield return null;
            }
        }

        private IEnumerator DecideBuildAir()
        {
            if (_lastAirPlatform == null)
            {
                _lastAirPlatform = BuildAirPlatform();
                yield return null;
            }
            
            if (_lastAirPlatform.getBounds().max.x + Random.Range(_world.AirPlatformGapMin, _world.AirPlatformGapMax) < _world.XSpawn)
            {
                _lastAirPlatform = BuildAirPlatform();
            }

            yield return null;
        }

        private ActorComponent BuildAirPlatform()
        {
            var platform = _actorFactory.buildAirPlatform(GenerationStrategy);
            AdjustToBounds(platform);

            float platformSizeX = platform.getRendererBounds().size.x;

            if (Random.Range(0, 1f) <= _world.EnemySpawnChancePlatforms)
            {

                float translate = Random.Range(-platformSizeX * 0.5f, platformSizeX * 0.5f);
                //SpawnActor(platform, _objFactory.buildEnemy("BUMPER"), translate);

            }

            if (Random.Range(0, 1f) <= _world.PickupSpawnChancePlatforms)
            {

                //float translate = Random.Range(-platformSizeX * 0.5f, platformSizeX * 0.5f);
                //SpawnActor(platform, _objFactory.buildPickup(PickupEffectType.STAR), 0);

            }

            return platform;
        }

        void AdjustToBounds(ActorComponent obj)
        {

            float max = obj.getBounds().max.y;
            float min = obj.getBounds().min.y;


            float diff = 0;
            if (max > _world.ScreenModel.CellingY)
            {
                diff = _world.ScreenModel.CellingY - max;
            }
            else if (min < _world.ScreenModel.FloorY)
            {
                diff = _world.ScreenModel.FloorY - min;
            }

            obj.transform.Translate(0, diff, 0);
        }

        private IEnumerator DecideBuildGround()
        {
            if (_lastGroundPlatform == null)
            {
                _lastGroundPlatform = BuildGroundPlatform(_groundType, _world.XSpawn);
                _groundType = FloorPlatformType.CENTER;
                yield return null;
            }
            
            if (_lastGroundPlatform.transform.position.x < _world.XSpawn)
            {
                var position =_lastGroundPlatform.transform.position.x + _world.FloorPlatformSize;

                _lastGroundPlatform = BuildGroundPlatform(_groundType, position);
                _groundType = FloorPlatformType.CENTER; // reset platform to default

            }

            yield return null;

        }

        private ActorComponent BuildGroundPlatform(FloorPlatformType type, float xPosition)
        {
            var platform = _actorFactory.buildGroundPlatform(type, xPosition);

            SpawnProp(platform, _actorFactory.buildProp(new Prop()));

            if (Random.Range(0, 1f) <= _world.EnemySpawnChanceGround)
            {
                var translate = Random.Range(-_world.FloorPlatformSize * 0.5f, _world.FloorPlatformSize * 0.5f);
                SpawnActor(platform, _actorFactory.buildEnemy("BUMPER"), translate);
            }

            if (Random.Range(0, 1f) <= _world.PickupSpawnChanceGround)
            {
                var translate = Random.Range(-_world.FloorPlatformSize * 0.5f, _world.FloorPlatformSize * 0.5f);
                SpawnActor(platform, _actorFactory.buildPickup(PickupEffectType.STAR), translate);
            }

            return platform;
        }

        private void OnChangeStrategy(PlatformGenerationStrategy previousStrategy,
            PlatformGenerationStrategy newStrategy)
        {
            Debug.Log($"Strategy Switch: {previousStrategy} to {newStrategy}");

            switch (previousStrategy)
            {
                case PlatformGenerationStrategy.GroundOnly:

                    _groundType = FloorPlatformType.RIGHT;
                    _lastGroundPlatform = null;
                    break;

                case PlatformGenerationStrategy.AirOnly:
                    _lastAirPlatform = null;
                    break;
            }

            switch (newStrategy)
            {
                case PlatformGenerationStrategy.GroundOnly:
                    //_lastBuiltGroundPlatform = BuildGroundPlatform(FloorPlatformType.LEFT, _world.XSpawn);
                    _groundType = FloorPlatformType.LEFT;
                    break;

                case PlatformGenerationStrategy.AirOnly:
                    break;

            }
        }
    }
}