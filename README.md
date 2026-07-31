# IPv4 Subnetting Calculator in C#

A clean, efficient, and command-line-based subnet calculator written in C#. This tool dynamically calculates and displays comprehensive subnet details for network classes **A**, **B**, and **C** using recursive functions and custom formatting.

## Features
- **Class-based Subnetting:** Automatically handles Class A, B, and C networks.
- **Recursive Generation:** Iterates through all possible CIDR notations for each class.
- **Detailed Metrics:** Calculates Subnet ID bits, Host ID bits, Subnets per network, Hosts per network, and Dotted Decimal Subnet Masks.
- **Clean CLI Formatting:** Displays tabular outputs nicely structured in the console.

## Code Structure
- `DisplayClassSubnetsInfo`: A recursive method to loop through CIDR blocks.
- `GetSubnetIDBits` & `GetHostIDBits`: Core networking logic for bit calculations.
- `GetSubnetMaskDotted`: Converts bit lengths into standard dotted-decimal IP format.

## How to Run
1. Make sure you have the [.NET SDK](https://dotnet.microsoft.com/) installed.
2. Clone this repository:
   ```bash
   git clone [https://github.com/qusai-netizen/IPv4-Subnetting-CLI-Tool.git](https://github.com/qusai-netizen/IPv4-Subnetting-CLI-Tool.git)