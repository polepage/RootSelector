using System;
using Android.OS;
using Android.Runtime;
using Droid.Utils.Serialization;
using Java.Interop;
using Root;

namespace RootSelector.Intents
{
    class FactionParcelable : Java.Lang.Object, IParcelable
    {
        private static readonly ParcelableCreator<FactionParcelable> _creator =
            new ParcelableCreator<FactionParcelable>(p => new FactionParcelable(p));

        [ExportField("CREATOR")]
        public static IParcelableCreator GetCreator() => _creator;

        public FactionParcelable() { }

        public FactionParcelable(Faction source)
        {
            Faction = source;
        }

        private FactionParcelable(Parcel parcel)
        {
            char id = Convert.ToChar(parcel.ReadInt());
            string name = parcel.ReadString();
            int reach = parcel.ReadInt();

            Faction = Faction.Create(id, name, reach);
        }

        public Faction Faction { get; private set; }

        public int DescribeContents() => 0;

        public void WriteToParcel(Parcel dest, [GeneratedEnum] ParcelableWriteFlags flags)
        {
            dest.WriteInt(Convert.ToInt32(Faction.Id));
            dest.WriteString(Faction.Name);
            dest.WriteInt(Faction.Reach);
        }
    }
}