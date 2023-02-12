EP_DIR = os.getenv("EP_DIR")

include (EP_DIR .. "/vendor/premake/premake_customization/solution_items.lua")

workspace "Chess"
	architecture "x86_64"
	startproject "Chess"

	configurations {
		"Debug",
		"Release",
		"Dist"
	}

	flags {
		"MultiProcessorCompile"
	}

project "Chess"
	kind "SharedLib"
	language "C#"
	dotnetframework "4.8"

	targetdir ("Binaries")
	objdir ("Intermediates")

	files {
		"Source/**.cs",
		"Properties/**.cs"
	}

	links {
		"EpScriptCore"
	}

	filter "configurations:Debug"
		optimize "Off"
		symbols "Default"

	filter "configurations:Release"
		optimize "On"
		symbols "Default"
	
	filter "configurations:Dist"
		optimize "Full"
		symbols "Off"

group "EpEngine"
	include (EP_DIR .. "/EpScriptCore") -- Don't include, just link with TODO!!
group ""
