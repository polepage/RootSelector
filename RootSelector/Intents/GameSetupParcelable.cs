using System.Linq;
using Android.OS;
using Android.Runtime;
using Droid.Utils.Serialization;
using Java.Interop;
using Root;

namespace RootSelector.Intents
{
    class GameSetupParcelable : Java.Lang.Object, IParcelable
    {
        private static readonly ParcelableCreator<GameSetupParcelable> _creator =
            new ParcelableCreator<GameSetupParcelable>(p => new GameSetupParcelable(p));

        [ExportField("CREATOR")]
        public static IParcelableCreator GetCreator() => _creator;

        public static readonly string IntentId = "root.gamesetup";

        public GameSetupParcelable() { }

        public GameSetupParcelable(GameSetup source)
        {
            GameSetup = source;
        }

        private GameSetupParcelable(Parcel parcel)
        {
            var seats = parcel.CreateTypedArray(FactionParcelable.GetCreator())
                .Cast<FactionParcelable>()
                .Select(fp => fp.Faction)
                .ToList();
            int firstPlayer = parcel.ReadInt();

            GameSetup = GameSetup.Create(seats, firstPlayer);
        }

        public GameSetup GameSetup { get; private set; }

        public int DescribeContents() => 0;

        public void WriteToParcel(Parcel dest, [GeneratedEnum] ParcelableWriteFlags flags)
        {
            dest.WriteTypedArray(GameSetup.Seats.Select(f => new FactionParcelable(f)).ToArray(), flags);
            dest.WriteInt(GameSetup.FirstPlayer);
        }
    }
}