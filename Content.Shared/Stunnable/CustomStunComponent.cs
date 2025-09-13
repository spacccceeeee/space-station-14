using Robust.Shared.GameStates;

namespace Content.Shared.Stunnable
{
    [RegisterComponent, NetworkedComponent, AutoGenerateComponentState]
    public sealed partial class CustomStunComponent : Component
    {
        [DataField("countToStun"), AutoNetworkedField]
        public int CountToStun = 3;

        [DataField("knockdownTime"), AutoNetworkedField]
        public float KnockdownTime = 3f;

        [DataField("stunTime"), AutoNetworkedField]
        public float StunTime = 3f;
    }
}
