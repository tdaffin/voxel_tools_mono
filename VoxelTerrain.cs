using Godot;
using System;

public class VoxelTerrain : Godot.VoxelTerrain
{
    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        // Intended to be pretty much a C# version of
        // https://github.com/Zylann/godot_voxel/blob/master/doc/06_custom-generator.md
        Stream = new ColorVoxelGenerator();
    }
}

public class ColorVoxelGenerator : VoxelGenerator
{
    public static readonly VoxelBuffer.ChannelId BlockChannel = VoxelBuffer.ChannelId.ChannelType;

    public override int GetUsedChannelsMask()
    {
        return 1 << (int)BlockChannel;
    }

    public override void GenerateBlock(VoxelBuffer outBuffer, Vector3 originInVoxels, int lod)
    {
        if (lod != 0)
            return;
        var ch = (uint)BlockChannel;

        var id = (uint)1;

        if (originInVoxels.y < 0)
            outBuffer.Fill(id, ch);

        if (originInVoxels.x == originInVoxels.z && originInVoxels.y < 1)
            outBuffer.Fill(id, ch);
    }
}
