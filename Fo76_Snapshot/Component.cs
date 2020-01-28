using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fo76_Snapshot
{
    public enum ComponentType : byte {
        ActorSnapshotComponent = 1,
        TESObjectREFRCoreComponent = 2,
        TESObjectREFRScriptObjectSnapshotComponent = 3,
        ActorValueSnapshotComponent = 4,
        DestructibleObjectSnapshotComponent = 10,
        ActorPerksSnapshotComponent = 11,
        TESObjectREFRPhysicsSnapshotComponent = 12,
        ProjectilePhysicsSnapshotComponent = 74,
        RemoteServerActorBehaviorComponent = 14,
        WorkshopNetworkSnapshotComponent = 15,
        PlayerCoreSnapshotComponent = 16,
        ExtraPowerLinksSnapshotComponent = 18,
        SnapshotComponentBGSBendableSpline = 19,
        ActorValuesSnapshotComponent = 20,
        TESObjectREFRTransformSnapshotComponent = 21,
        ActorPathComponent = 25,
        RemoteServerActorMotionComponent = 28,
        ActorPowerArmorWorkbenchComponent = 30,
        RadioSoundComponent = 64,
        RadioFileComponent = 65,
        PlayerPrivateCoreSnapshotComponent = 66,
        RemoteServerActorProjectileComponent = 68,
        SnapshotComponentExtraPowerArmorOwnership = 69,
        RadioReceiverComponent = 70,
        AnimationProgressComponent = 71,
        ExtraDataCoreSnapshotComponent = 72,
        ExtraKeypadDataSnapshotComponent = 73,
        TESObjectREFRQuestItemSnapshotComponent = 75,
        ClientPlayerCoreSnapshotComponent = 79,
        QuickPlayBabylonDataComponent = 177,
        SnapshotComponentExtraPowerArmor = 82,
        SnapshotComponentExtraPlayerStorage = 105,

        ClientInventoryItemComponent = 233, //0xE9 - 0xFB //14000000013329E2
        ClientInventoryResourceComponent = 252, //0xFC - 0xFE
    }

    class Component
    {
        uint EntityId;
        uint RessourceId;
        uint ComponentSize;
        uint ComponentData;
        ComponentType ComponentId;
        byte[] ComponentBuffer;

        public Component(uint EntityId, uint RessourceId, uint ComponentSize, uint ComponentId, byte[] ComponentData) {
            this.EntityId = EntityId;
            this.RessourceId = RessourceId;
            this.ComponentSize = ComponentSize;
            this.ComponentId = (ComponentType)ComponentId;
            this.ComponentBuffer = ComponentData;
        }

        public override string ToString()
        {
            return " EntityId:" + EntityId.ToString("X") + " RessourceId:" + RessourceId.ToString("X") + " ComponentSize:" + ComponentSize.ToString("X") + " ComponentId:" + ComponentId;
        }
    }
}
