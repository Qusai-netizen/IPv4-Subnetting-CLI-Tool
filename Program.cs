enum netClass
{
    A = 8,
    B = 16,
    C = 24
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

    static void DisplayClassSubnetsInfo(netClass cl, int _cidr = 0)
    {
        int cidr = _cidr;

        if (cidr == 0)
        {
            DisplayTableHead();
            cidr = (int)cl;
        }
        else if (cidr == 33)
        {
            Console.Write("\n\n\n");
            return;
        }

        DisplayRow(
            GetSubnetIDBits(cl, cidr),
            GetHostIDBits(cidr),
            GetSubnetsPerNetwork(GetSubnetIDBits(cl, cidr)),
            GetHostsPerNetwork(GetHostIDBits(cidr)),
            GetSubnetMaskDotted(cidr),
            GetSubnetMaskBinary(cidr),
            "/" + cidr
        );

        DisplayClassSubnetsInfo(cl, cidr + 1);
    }

    static void DisplayTableHead()
    {
        // One dash for each wall, to spaces for each element, 10 characters for five elements and 35 for the sixth
        Console.WriteLine("--------------------------------------------------------------------------------------------------------");
        Console.WriteLine("| Subnet ID  | Host ID    | Subnet per | Hosts per  | Subnet mask                         | CIDR       |");
        Console.WriteLine("| bits       | bits       | network    | network    |                                     |            |");
        Console.WriteLine("--------------------------------------------------------------------------------------------------------");
    }

    static void DisplayRow(int subnetIDBits, int hostIDBits, int subnetsPerNetork, int hostsPerNetwork, string subnetMaskDotted, string subnetMaskBinary, string cidr)
    {
        Console.WriteLine($"| {subnetIDBits,-10} | {hostIDBits,-10} | {subnetsPerNetork,-10} | {hostsPerNetwork,-10} | {subnetMaskDotted,-35} | {cidr,-10} |");
        Console.WriteLine($"|            |            |            |            | {subnetMaskBinary,-35} |            |");
        Console.WriteLine("--------------------------------------------------------------------------------------------------------");
    }

    static int GetSubnetIDBits(netClass cl, int cidrVal)
    {
        return cidrVal - (int)cl; // CIDR vlue - the number of network bits in the class
    }

    static int GetHostIDBits(int cidrVal)
    {
        return 32 - cidrVal;
    }

    static int GetSubnetsPerNetwork(int subnetIDBits)
    {
        return (int)Math.Pow(2, subnetIDBits);
    }

    static int GetHostsPerNetwork(int hostIDBits)
    {
        return (int)Math.Pow(2, hostIDBits) - 2;
    }

    static string GetSubnetMaskDotted(int cidrVal)
    {
        string[] octets = new string[4];

        int i = 0;
        for (; i < cidrVal / 8; ++i)
            octets[i] = "255";

        int remainingBits = cidrVal % 8;
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

    static int BinaryToDecimal8Bits(int numberOfOnesFromLeft)
    {
        int sum = 0;
        while (numberOfOnesFromLeft > 0)
            sum += (int)Math.Pow(2, 8 - numberOfOnesFromLeft--);

        return sum;
    }

    static string GetSubnetMaskBinary(int cidrVal)
    {
        char[] subnetMask = new char[35];

        int onesCounter = 0;
        for (int i = 0; i < 35; ++i)
        {
            if (i == 8 || i == 17 || i == 26)
            {
                subnetMask[i] = '.';
                continue;
            }
            if (onesCounter < cidrVal)
            {
                ++onesCounter;
                subnetMask[i] = '1';
            }
            else
            {
                subnetMask[i] = '0';
            }
        }
        return new string(subnetMask);
    }
}