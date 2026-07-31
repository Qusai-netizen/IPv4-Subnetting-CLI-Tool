enum netClass
{
    A,
    B,
    C
}

class Subnet
{

    static void Main()
    {
        netClass cl = netClass.A;
        DisplayClassSubnetsInfo(cl);

        cl = netClass.B;
        DisplayClassSubnetsInfo(cl);

        cl = netClass.C;
        DisplayClassSubnetsInfo(cl);

    }

    static void DisplayClassSubnetsInfo(netClass cl, string _cidr = "")
    {
        string cidr = _cidr;

        if (cidr == "")
        {
            cidr = cl switch
            {
                netClass.A => "/8",
                netClass.B => "/16",
                netClass.C => "/24",
                _ => "/32"
            };
        }
        else if (cidr == "/33")
        {
            Console.Write("\n\n\n");
            return;
        }

        if (_cidr == "")
        {
            DisplayTableHead();
        }

        DisplayRow(
            GetSubnetIDBits(cl, cidr),
            GetHostIDBits(cidr),
            GetSubnetsPerNetwork(GetSubnetIDBits(cl, cidr)),
            GetHostsPerNetwork(GetHostIDBits(cidr)),
            GetSubnetMaskDotted(cidr),
            cidr
        );

        string nextCidr = "/" + (int.Parse(cidr.Replace("/", "").Trim()) + 1);
        DisplayClassSubnetsInfo(cl, nextCidr);
    }

    static void DisplayTableHead()
    {
        // One dash for each wall, to spaces for each element, 10 characters for five elements and 25 for the sixth
        Console.WriteLine("----------------------------------------------------------------------------------------------");
        Console.WriteLine("| Subnet ID  | Host ID    | Subnet per | Hosts per  | Subnet mask               | CIDR       |");
        Console.WriteLine("| bits       | bits       | network    | network    |                           |            |");
        Console.WriteLine("----------------------------------------------------------------------------------------------");
    }

    static void DisplayRow(int subnetIDBits, int hostIDBits, int subnetsPerNetork, int hostsPerNetwork, string subnetMaskDotted, string cidr)
    {
        Console.WriteLine($"| {subnetIDBits,-10} | {hostIDBits,-10} | {subnetsPerNetork,-10} | {hostsPerNetwork,-10} | {subnetMaskDotted,-25} | {cidr,-10} |");
        Console.WriteLine($"|            |            |            |            | {"working on it",-25} |            |");
        Console.WriteLine("----------------------------------------------------------------------------------------------");
    }

    static int GetSubnetIDBits(netClass cl, string cidr)
    {
        string bits = cidr.Remove(0, 1);
        int cidrNetworkBits;

        bool success = int.TryParse(bits, out cidrNetworkBits);

        if (success)
        {
            int networkBits = cl switch
            {
                netClass.A => 8,
                netClass.B => 16,
                netClass.C => 24,
                _ => 24
            };

            return cidrNetworkBits - networkBits;
        }
        else
        {
            return -1;
        }
    }

    static int GetHostIDBits(string cidr)
    {
        string bits = cidr.Remove(0, 1);
        int cidrNetworkBits;

        bool success = int.TryParse(bits, out cidrNetworkBits);

        if (success)
        {
            return 32 - cidrNetworkBits;
        }
        else
        {
            return -1;
        }
    }

    static int GetSubnetsPerNetwork(int subnetIDBits)
    {
        return (int)Math.Pow(2, subnetIDBits);
    }

    static int GetHostsPerNetwork(int hostIDBits)
    {
        return (int)Math.Pow(2, hostIDBits) - 2;
    }

    static string GetSubnetMaskDotted(string cidr)
    {
        string bits = cidr.Remove(0, 1);
        int cidrNetworkBits;

        bool success = int.TryParse(bits, out cidrNetworkBits);

        if (success)
        {
            string[] octets = new string[4];

            int i = 0;
            for (; i < cidrNetworkBits / 8; ++i)
            {
                octets[i] = "255";
            }

            int remainingBits = cidrNetworkBits % 8;
            for (; i < 4; ++i)
            {
                if (remainingBits == 0)
                {
                    octets[i] = "0";
                }
                else
                {
                    octets[i] = BinaryToDecimal8Bits(remainingBits).ToString();
                    remainingBits = 0;
                }
            }

            return $"{octets[0]}.{octets[1]}.{octets[2]}.{octets[3]}";
        }
        else
        {
            return "failed";
        }
    }

    static int BinaryToDecimal8Bits(int numberOfOnesFromLeft)
    {
        int sum = 0;
        while (numberOfOnesFromLeft > 0)
        {
            sum += (int)Math.Pow(2, 8 - numberOfOnesFromLeft);
            --numberOfOnesFromLeft;
        }
        return sum;
    }
}