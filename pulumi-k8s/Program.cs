using System.Collections.Generic;
using System.Threading.Tasks;

using Pulumi;
using Pulumi.Kubernetes.Core.V1;
using Pulumi.Kubernetes.Apps.V1;
using Pulumi.Kubernetes.Types.Inputs.Core.V1;
using Pulumi.Kubernetes.Types.Inputs.Apps.V1;
using Pulumi.Kubernetes.Types.Inputs.Meta.V1;

class Program
{
    static Task<int> Main()
    {
        return Pulumi.Deployment.RunAsync(() =>
        {
            //1. Get existed cluster from eks
            var existedCluster = Pulumi.Aws.Eks.Cluster.Get("your-eks-cluster", "your-eks-cluster");

            //2. Create new deployment
            var cqrsTodoDeployment = new Pulumi.Kubernetes.Apps.V1.Deployment("cqrstodo-deployment", new DeploymentArgs
            {
                Metadata = new ObjectMetaArgs
                {
                    Name = "cqrstodo-deployment",
                    Labels = new InputMap<string> { { "app", "cqrstodo" } }
                },
                Spec = new DeploymentSpecArgs
                {
                    Replicas = 1,
                    Selector = new LabelSelectorArgs
                    {
                        MatchLabels = new InputMap<string> { { "app", "cqrstodo" } }
                    },
                    Template = new PodTemplateSpecArgs
                    {
                        Metadata = new ObjectMetaArgs
                        {
                            Name = "cqrstodo",
                            Labels = new InputMap<string> { { "app", "cqrstodo" } }
                        },
                        Spec = new PodSpecArgs
                        {
                            Containers = new InputList<ContainerArgs>{
                                new ContainerArgs{
                                    Name="cqrstodo",
                                    Image="ecr-url",
                                    Ports=new ContainerPortArgs{ContainerPortValue=80}
                                }
                            }
                        }
                    }
                },

            });
            var output = new Dictionary<string, object>
            {
                { "cluster", existedCluster.Name },
                { "pod-deployment", cqrsTodoDeployment.GetResourceName()}
            };

            return output;
        });
    }
}
