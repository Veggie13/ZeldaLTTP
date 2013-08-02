using System;

using Axiom.Core;
using Axiom.Math;
using Axiom.Graphics;

namespace ZeldaLTTP
{
    public sealed class Game
    {
        private readonly Root _root;
        private readonly RenderWindow _window;
        private SceneManager _scene;
        private Camera _camera;

        public Game(Root root, RenderWindow window)
        {
            _root = root;
            _window = window;
        }

        public void OnLoad()
        {
            ResourceGroupManager.Instance.AddResourceLocation("media\\Meshes", "Folder");
            ResourceGroupManager.Instance.AddResourceLocation("media\\Materials", "Folder");

            _scene = _root.CreateSceneManager("DefaultSceneManager", "DefaultSM");
            _scene.ClearScene();

            _camera = _scene.CreateCamera("MainCamera");

            _camera.Position = new Vector3(0, 10, 500);
            _camera.LookAt(Vector3.Zero);
            _camera.Near = 5;

            var vp = _window.AddViewport(_camera);
            vp.BackgroundColor = ColorEx.Black;
            _camera.AspectRatio = (float)vp.ActualWidth / vp.ActualHeight;

            ResourceGroupManager.Instance.InitializeAllResourceGroups();

        }

        public void CreateScene()
        {
            _scene.AmbientLight = ColorEx.Black;
            _scene.ShadowTechnique = ShadowTechnique.StencilAdditive;

            Entity ent = _scene.CreateEntity("ninja", "penguin.mesh");
            ent.CastShadows = true;
            SceneNode node = _scene.RootSceneNode.CreateChildSceneNode("HeadNode");
            node.AttachObject(ent);

            Plane plane = new Plane(Vector3.UnitY, 0);
            MeshManager.Instance.CreatePlane("ground",
                ResourceGroupManager.DefaultResourceGroupName, plane,
                1500, 1500, 20, 20, true, 1, 5, 5, Vector3.UnitZ);
            Entity groundEnt = _scene.CreateEntity("GroundEntity", "ground");
            _scene.RootSceneNode.CreateChildSceneNode().AttachObject(groundEnt);
            groundEnt.MaterialName = "Rockwall";
            groundEnt.CastShadows = false;
        }

        public void OnUnload()
        {
        }

        public void OnRenderFrame(object s, FrameEventArgs e)
        {
        }

    }
}
