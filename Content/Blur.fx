sampler TextureSampler : register(s0);

float2 pixelSize;

float4 GaussianBlurPS(float2 texCoord : TEXCOORD0) : COLOR0
{
    float4 color = float4(0, 0, 0, 0);

    float2 offsets[9] =
    {
        -pixelSize * 15.0,
        -pixelSize * 10.0,
        -pixelSize * 5.0,
        -pixelSize * 2.5,
         float2(0, 0),
         pixelSize * 2.5,
         pixelSize * 5.0,
         pixelSize * 10.0,
         pixelSize * 15.0
    };

    float weights[9] =
    {
        0.05, 0.09, 0.12, 0.15, 0.16, 0.15, 0.12, 0.09, 0.05
    };

    for (int i = 0; i < 9; i++)
    {
        color += tex2D(TextureSampler, texCoord + offsets[i]) * weights[i];
    }

    return color;
}

technique Technique1
{
    pass Pass1
    {
        PixelShader = compile ps_2_0 GaussianBlurPS();
    }
}