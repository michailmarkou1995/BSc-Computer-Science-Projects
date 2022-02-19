# Advanced_VOIP_VoiceChat_System
Advanced Audio/Voice Signal Spectrum/Band Routing for Multiplayer Video Games Blueprint System in Unreal Engine<br/>

![#f03c15](https://via.placeholder.com/15/f03c15/000000?text=+) `This Product will be available on Unreal MarketPlace`

```
**NO Sound middleware depended e.g., FMOD, Wwise**
**100% Blueprints NO C++ required**
```

```
3 Systems Seperate will also be available as a product. (Business(Product-Engineer-Market))

1) Simple VOIP Voice Chat (team/global/positional-proximity) 5.99
2) Walkie Talkie VOIP Voice Chat 10.99
3) Cellphone VOIP Voice Chat 10.99
4) And this Advanced VOIP Voice Chat System 30.99
```

# Main Features:<br/>
• Proximity/Positional Distance Voice Chat<br/>
• Global Voice Chat (2 Methods)<br/>
• Team Voice Chat<br/>
• Mute-Player/s<br/>
• Walkie-Talkie SingleBand (mono-Frequency) + Proximity/Positional Attenuation Distance Near Player<br/>
• Walkie-Talkie MultiBand (multi-Frequencies) + Proximity/Positional Attenuation Distance Near Player<br/>
• Routing Voice to Speakers assets inside 3D environment aka "the singer effect" hearing live signal feedback (VOIP Audio/sound Real-time conversion to 3D Location Based sound)<br/>
• SinglePlayer Voice Routing to Speaker Assets without the need for Multiplayer Session initiated (same as above bullet but without the session)<br/>
• Cellphone Call circuit (A player can call any other player and talk privately with proximity Audio Distance from Phone and players Position Voice)<br/>
-  Cellphone 2 Categories<br/>
1) Circuit Information on the player (Data in the memory of the Player)<br/>
2) Circuit Information on the phone (Data in the SIM card of the phone)<br/>

## Walkie-Talkie States:<br/>
• OFF (no receive no broadcast)<br/>
• ON (receive but no broadcast)<br/>
• ON (receive and broadcast)<br/>

## Cellphone States:<br/>
• OFF<br/>
• ON<br/>
• ON + Receive Call to answer (Circuit-Setup) + Busy Circuit if on call already<br/>
• ON Make Call to a certain Player (Circuit-Setup) + Busy Circuit if on call already<br/>

## Frequencies:<br/>
• A player can pick a Frequency FM and only in that Frequency can talk to other players with synched same Hz<br/>

## Additional mechanics:<br/>
• Multiplayer LAN Session Client-Server<br/>
• PickUp/Drop Actor System<br/>
• Locomotion States<br/>
• NPC A.I Detection by Hearing for testing<br/>
• Niagara Visualizer Audio Spectrum/Band-freq Analysis/Response Waveform kinetics<br/>
• Basic Graphics Quality Settings<br/>
• Team Selection Screen<br/>
• Walkie-Talkie Screen Emission on different states<br/>
• Player Name-Tags with proper repNotify for late join-games<br/>
• Works on Late join-games as well without interruption to new or old players<br/>
• Cellphone + Walkie-Talkie SoundFX + Calling/Dial RingTones + Areas Boxes (no event Tick! but Live update between switching while talking) with Signal Audio Attenuation Low Bar quality/static Noise and Signal Corruption (ideal for Horror Multiplayer Games e.g., Attach it on areas or Enemys when nearby your Audio will get low quality while talking with others or even complete noise static corruption From Both Receiver/s and Sender/s Perspective while other players remain untouched to signal degradation - yes evil presence interrupts comms :) )<br/>
• Volume/Mic Input/Output Sensitivity Adjust<br/>
• 2D/3D Sound<br/>
• Compatible with any UE4 Online Subsystem using UE4 standard network API (RPC, replicated variables/RepNotifies)<br/>
• Cross-Platform<br/>
