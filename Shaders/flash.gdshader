shader_type canvas_item;

uniform float flash_strength : hint_range(0.0, 1.0) = 0.0;

void fragment() {
    vec4 base_color = texture(TEXTURE, UV);

    // Mix between the base color and pure white
    vec3 flashed = mix(base_color.rgb, vec3(1.0, 1.0, 1.0), flash_strength);

    COLOR = vec4(flashed, base_color.a);
}
