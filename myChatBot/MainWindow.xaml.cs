using Google.Protobuf;
using K4os.Compression.LZ4.Streams;
using Microsoft.VisualBasic;
using myChatBot;
using MySql.Data.MySqlClient;
using MySqlX.XDevAPI;
using Org.BouncyCastle.Utilities;
using System.Printing;
using System.Speech.Synthesis;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Xml.Linq;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace myChatBot
{

    public partial class MainWindow : Window
    {
        public static string name = "";
        public static string Info = "";
        public static int counter = 1;
        public static int TotalMarks = 0;
        public static string UserTible = "";
        public static string UserDescriprion = "";
        public static int UserDueDate = 0;
        public static List<string> conversationHistory = new List<string>();
        public static string art = @"                            _______
                          _/       \_
                         / |       | \
                        /  |__   __|  \
                       |__/((o| |o))\__|
                       |      | |      |
                       |\     |_|     /|
                       | \           / |
                        \| /  ___  \ |/
                         \ | / _ \ | /
                          \_________/
                           _|_____|_
                          |         |
====================================================================================
             ██████╗██╗   ██╗██████╗ ███████╗██████╗ 
            ██╔════╝╚██╗ ██╔╝██╔══██╗██╔════╝██╔══██╗
            ██║      ╚████╔╝ ██████╔╝█████╗  ██████╔╝
            ██║       ╚██╔╝  ██╔══██╗██╔══╝  ██╔══██╗
            ╚██████╗   ██║   ██████╔╝███████╗██║  ██║
             ╚═════╝   ╚═╝   ╚═════╝ ╚══════╝╚═╝  ╚═╝
====================================================================================
   ███████╗███████╗ ██████╗██╗   ██╗██████╗ ██╗████████╗██╗   ██╗
   ██╔════╝██╔════╝██╔════╝██║   ██║██╔══██╗██║╚══██╔══╝╚██╗ ██╔╝
   ███████╗█████╗  ██║     ██║   ██║██████╔╝██║   ██║    ╚████╔╝ 
   ╚════██║██╔══╝  ██║     ██║   ██║██╔══██╗██║   ██║     ╚██╔╝  
   ███████║███████╗╚██████╗╚██████╔╝██║  ██║██║   ██║      ██║   
   ╚══════╝╚══════╝ ╚═════╝ ╚═════╝ ╚═╝  ╚═╝╚═╝   ╚═╝      ╚═╝
====================================================================================
                            Awareness Assistant
====================================================================================

";
        public static Dictionary<string, string> TipsMemory = new Dictionary<string, string>()
            {
                { "phishing tip", "Never click links in unexpected emails. Hover over links to see the real URL. When in doubt, contact the sender directly by phone to verify." },

                { "phishing email tip", "Look for red flags: poor grammar, urgent requests, mismatched email addresses, and suspicious attachments." },

                 {"worried","I'm here to help "+name+" What seems to be the problem?" },

                {"curious","Im happy to hear that "+name+". What would you like to know about cybersecurity?"},

                {"frustrated","I understand you're feeling frustrated "+name+". How can I assist you?"},
// Password tips
             
                { "password tip", "Create strong passwords with at least 12 characters, mixing uppercase, lowercase, numbers, and symbols. Never reuse passwords!" },

                { "strong password tip", "Use passphrases like 'PurpleElephant$JumpsOver3Times!' - they're long, memorable, and hard to crack." },

                { "password manager tip", "Use a password manager like Bitwarden, LastPass, or 1Password to generate and store unique passwords securely." },

// Cybersecurity tips
                { "cybersecurity tip", "Enable 2FA everywhere possible, keep software updated, use antivirus, and think before you click!" },

                { "general security tip", "Always lock your computer when stepping away, even at home. Use a screensaver with password protection." },

// Malware tips
              { "malware tip", "Don't download software from untrusted sources. Scan USB drives before opening. Keep your antivirus updated!" },

                { "ransomware tip", "Maintain offline backups using the 3-2-1 rule: 3 copies, 2 different media types, 1 offsite backup. Never pay the ransom!" },

                { "virus tip", "Scan all email attachments before opening, disable macros in Office files, and keep Windows Defender active." },

                { "trojan tip", "Only download software from official websites. Pirated software and crack tools often contain trojans." },

                { "spyware tip", "Use anti-spyware tools, avoid clicking pop-up ads, and regularly review your browser extensions." },

                { "keylogger tip", "Use on-screen keyboard for entering passwords on public computers. Keep antivirus updated to detect keyloggers." },

// Network security tips
            
                { "firewall tip", "Keep your firewall enabled even on private networks. It's your first line of defense against unauthorized access." },

                { "public wifi tip", "Never access banking or sensitive accounts on public Wi-Fi. Always use a VPN on public networks!" },

                { "vpn tip", "Use a paid, no-log VPN service like ProtonVPN, Mullvad, or ExpressVPN. Free VPNs often sell your data." },

                { "router tip", "Change your router's default password, disable WPS, enable WPA3 encryption, and update firmware regularly." },

                { "ddos tip", "Use Cloudflare or similar services for DDoS protection. Small businesses are also targets, not just big companies." },

// Authentication tips
                { "2fa tip", "Always enable two-factor authentication. Use authenticator apps like Google Authenticator or Authy instead of SMS when possible." },

                { "mfa tip", "Combine something you know (password) + something you have (phone) + something you are (fingerprint) for maximum security." },

                { "authentication tip", "Never share your 2FA codes with anyone - not even 'tech support' who calls you!" },

// Data protection tips
                { "encryption tip", "Encrypt sensitive files before cloud upload. Use VeraCrypt for folders or BitLocker for entire drives." },

                { "backup tip", "Follow the 3-2-1 backup rule: 3 copies, 2 different media types, 1 offsite backup. Test your restores regularly!" },

                { "data breach tip", "Use 'Have I Been Pwned' to check if your email was in a breach. Change passwords immediately if yes!" },

// Social engineering tips
                { "social engineering tip", "Never give passwords, 2FA codes, or personal info to anyone who calls you unexpectedly. Hang up and call back using an official number." },

                { "identity theft tip", "Freeze your credit with major bureaus (Equifax, Experian, TransUnion), monitor bank statements weekly, and shred sensitive documents." },

// Web security tips
                { "https tip", "Look for the padlock icon in your browser's address bar. Never enter passwords on HTTP sites - they're insecure!" },

                { "secure website tip", "Check for 'https://' and the padlock before entering any personal information online. Click the padlock to verify the certificate." },

// Email tips
                { "spam tip", "Never unsubscribe from suspicious emails - it confirms your address is active to spammers. Use your email's spam filters." },

                { "scam tip", "If something sounds too good to be true, it is! Never send money, gift cards, or cryptocurrency to online strangers." },

// Software tips
                { "antivirus tip", "Windows Defender is good for most home users. Keep it updated and run regular full system scans." },

                { "software update tip", "Enable automatic updates for your operating system, browsers, and all apps. Delaying updates leaves you vulnerable." },

// Privacy tips
                { "privacy tip", "Review your privacy settings on social media. Limit what you share publicly - oversharing helps attackers build profiles about you." },

                { "digital footprint tip", "Google yourself regularly. Remove old accounts and posts that reveal too much personal information." },

// Home security tips
                 { "iot security tip", "Keep IoT devices (smart cameras, smart fridges, Alexa) on a separate guest Wi-Fi network from your main computers." },

                { "smart home tip", "Change default passwords on all smart devices. Many come with 'admin/admin' - hackers scan for these!" },

// Business tips
                { "incident response tip", "Create an incident response plan: Detect → Contain → Eradicate → Recover → Learn. Practice with tabletop exercises." },

                { "risk management tip", "Identify your most valuable data, assess threats, and implement controls based on risk level (low/medium/high)." },

                { "security policy tip", "Document your security policies and train all employees. The weakest link is often human error." },

// Additional useful tips
                { "cloud security tip", "Encrypt files before uploading to cloud storage. Enable 2FA on your cloud accounts. Don't store sensitive data unencrypted." },

                { "mobile security tip", "Keep your phone updated, only install apps from official stores, and avoid sideloading apps from unknown sources." },

                { "email security tip", "Verify sender email addresses carefully. Scammers spoof 'From' addresses to look legitimate." },

                { "browser security tip", "Keep your browser updated, use uBlock Origin for ad blocking, and don't save passwords in your browser." },

                { "wifi security tip", "Change your default SSID (network name), use WPA3 or WPA2 encryption, and hide your SSID from broadcasting." },

                { "physical security tip", "Lock your devices when not in use. Use a privacy screen in public places. Don't leave laptops unattended." }
            };

        public static Dictionary<string, string> memory = new Dictionary<string, string>()
            {
            // General Cybersecurity questions

               {"how are you","I'm good. Thanks for asking. How can i help you?"},


               {"hey","Hello!! Welcome to the Cybersecurity Awareness Bot. how cam i help you"},

               {"hello","Hello!! Welcome to the Cybersecurity Awareness Bot. how cam i help you"},

               {"worried","I'm here to help "+name+" What seems to be the problem?" },

                {"curious","Im happy to hear that "+name+". What would you like to know about cybersecurity?"},

                {"frustrated","I understand you're feeling frustrated "+name+". How can I assist you?"},


               {"cybersecurity","Cybersecurity is the practice of protecting systems, networks, and data from digital attacks."},

               {"password","A password is a secret word or phrase used to access an account or system."},

               {"phishing","Phishing is a scam where attackers trick users into giving sensitive information."},

               {"malware","Malware is harmful software designed to damage or access systems without permission."},

               {"ransomware","Ransomware is malware that locks files and demands payment to unlock them."},

               {"virus","A virus is a type of malware that spreads by attaching itself to files."},

               {"worm","A worm is malware that spreads automatically through networks."},

               {"trojan","A Trojan is malware disguised as legitimate software."},

               {"firewall","A firewall is a system that blocks unauthorized access to or from a network."},

               {"encryption","Encryption is the process of converting data into a secure code."},

               {"decryption","Decryption is converting encrypted data back into readable form."},

               {"authentication","Authentication is the process of verifying a user's identity."},

               {"authorization","Authorization determines what a user is allowed to access."},

               {"multi factor authentication?","It is a security process that requires multiple methods to verify identity."},

               {"two factor authentication","It is a type of authentication using two verification methods."},

               {"data breach","A data breach is when sensitive information is accessed without permission."},

               {"hacker","A hacker is a person who gains unauthorized access to systems."},

               {"ethical hacking","Ethical hacking is legal hacking used to find and fix security weaknesses."},

               {"spyware","Spyware is malware that secretly collects user information."},

               {"adware","Adware is software that displays unwanted advertisements."},

               {"keylogger","A keylogger records keystrokes to capture sensitive information."},

               {"botnet","A botnet is a network of infected computers controlled by an attacker."},

               {"ddos attack","A DDoS attack overwhelms a system with traffic to make it unavailable."},

               {"vpn","A VPN is a secure connection that protects your internet activity."},

               {"https","HTTPS is a secure version of HTTP that encrypts web traffic."},

               {"http","HTTP is a protocol used to transfer data over the internet."},

               {"secure website","A secure website uses encryption to protect user data."},

               {"identity theft","Identity theft is stealing personal information for fraud."},

               {"social engineering","Social engineering tricks people into revealing confidential information."},

               {"cyberattack","A cyberattack is an attempt to damage or access systems illegally."},

               {"cyber threat","A cyber threat is a potential danger to systems or data."},

               {"antivirus","Antivirus software detects and removes malicious programs."},

               {"software update","A software update fixes bugs and security vulnerabilities."},

               {"backup","A backup is a copy of data stored for recovery."},

               {"cloud security","Cloud security protects data stored online."},

               {"network security","Network security protects computer networks from threats."},

               {"information security","Information security protects all forms of data."},

               {"public wi-fi","Public Wi-Fi is a shared internet connection that may be insecure."},

               {"private network","A private network is restricted and secure from public access."},

               {"router","A router directs internet traffic between devices."},

               {"ip address","An IP address identifies a device on a network."},

               {"domain name","A domain name is the address of a website."},

               {"spam","Spam is unwanted or harmful messages sent online."},

               {"scam","A scam is a fraudulent scheme to steal money or information."},

               {"cybercrime","Cybercrime is illegal activity done using computers or the internet."},

               {"digital footprint","A digital footprint is the data you leave behind online."},

               {"privacy","Privacy is the protection of personal information."},

               {"security policy","A security policy is a set of rules to protect systems and data."},

               {"incident response","Incident response is how organizations handle cyberattacks."},

               {"risk management","Risk management is identifying and reducing cybersecurity risks."},

               {"strong password","Use at least 12 characters including letters, numbers, and symbols." },

               {"password manager","A password manager securely stores and generates strong passwords." },

               {"weak password","Short passwords, common words, or personal information make passwords weak." },

                {"my name","Your name is " + name + ". I've never heard that name before but it's abolutely unique!"},
                //Tips for each cybersecurity topics
            
              // Additional question that might be asked

               {"what is your purpose","The chatbot teaches users about cyber threats such as phishing, malware, hacking, and identity theft. It also encourages good cybersecurity practices like strong passwords and avoiding suspicious links."},

               {"purpose","The chatbot teaches users about cyber threats such as phishing, malware, hacking, and identity theft. It also encourages good cybersecurity practices like strong passwords and avoiding suspicious links."},

               {"what can i ask you about","You can ask me general cybersecurity questions and how to stay safe online."},

               {"about","You can ask me general cybersecurity questions and how to stay safe online."},

               {"what is cybersecurity","Cybersecurity is the practice of protecting computers, networks, systems, and data from cyberattacks and unauthorized access."},

               {"why is cybersecurity important","Cybersecurity protects personal information, financial data, and business systems from being stolen or damaged."},

               {"important","Cybersecurity protects personal information, financial data, and business systems from being stolen or damaged." },

               {"what are the most common cyber threats today","Common cyber threats include phishing, malware, ransomware, identity theft, and social engineering attacks."},

               {"how can i protect my personal information online","Use strong passwords, enable two-factor authentication, avoid suspicious links, and only share information on trusted websites."},

               {"what is the difference between cybersecurity and information security","Cybersecurity protects digital systems and networks, while information security protects all types of information."},

               {"what is a cyberattack","A cyberattack is an attempt to access, damage, or steal data from a computer system or network."},

              {"who are hackers and what do they do","Hackers try to gain unauthorized access to computer systems. Some are criminals while others help organizations find security weaknesses."},

              {"what are the basic principles of cybersecurity","The main principles are confidentiality, integrity, and availability of data."},

              {"how do companies protect their data from cyber threats","Companies use firewalls, encryption, antivirus software, security policies, and employee training."},

              {"what are the biggest cybersecurity risks for individuals","Weak passwords, phishing emails, unsafe downloads, and unsecured public Wi-Fi."},


                 // Password and Authentication

              {"how do i create a strong password","Use at least 12 characters including letters, numbers, and symbols."},

              {"why should i avoid using the same password for multiple accounts","If one account gets hacked, attackers may access your other accounts."},

              {"what is multi factor authentication","It requires more than one way to verify your identity, like a password and a code sent to your phone."},

              {"why is two factor authentication important","It adds an extra layer of security and makes hacking more difficult."},

              {"how often should i change my password","You should change important passwords every 3 to 6 months."},

              {"what is a password manager","A password manager securely stores and generates strong passwords."},

              {"is it safe to store passwords in my browser","It can be convenient but a dedicated password manager is usually safer."},

              {"what makes a password weak","Short passwords, common words, or personal information make passwords weak."},

              {"what should i do if my password is hacked","Change it immediately and enable two-factor authentication."},

              {"how can i remember strong passwords","Use passphrases or store them in a password manager."},



    // Phishing

              {"what is phishing","Phishing is a scam used to trick people into revealing sensitive information."},

              {"how can i identify a phishing email","Look for suspicious links, unknown senders, poor grammar, or urgent requests."},

              {"what should i do if i receive a suspicious email","Do not click links or attachments. Delete or report the email."},

              {"can phishing happen through sms messages","Yes, it is called smishing."},

              {"what is spear phishing","A targeted phishing attack aimed at a specific person or organization."},

              {"how do hackers use phishing to steal information","They send fake emails or create fake websites to trick users."},

             {"what is a fake website and how can i identify one","Check the website address, spelling, and if it uses HTTPS."},

             {"what should i do if i accidentally click a phishing link","Close the page and scan your device with antivirus software."},



    // Malware

             {"what is malware","Malware is malicious software designed to harm or access systems without permission."},

             {"what is the difference between a virus worm and trojan","A virus spreads through files, a worm spreads through networks, and a Trojan hides inside legitimate software."},

             {"how can malware infect my computer","Through infected downloads, email attachments, or unsafe websites."},

             {"what is ransomware","Ransomware locks your files and demands payment to unlock them."},

             {"what should i do if my computer gets infected with malware","Disconnect from the internet and run antivirus software."},

             {"how can antivirus software protect my device","It scans and removes malicious programs."},

             {"can smartphones get viruses","Yes, especially from unsafe apps or downloads."},

             {"how can i safely download files from the internet","Download only from trusted websites."},



    // Internet Safety

             {"is public wi-fi safe to use","Public Wi-Fi can be risky. Avoid accessing sensitive accounts."},

             {"how can i stay safe on social media","Use privacy settings and avoid sharing personal information."},

             {"what personal information should i avoid sharing online","Avoid sharing addresses, ID numbers, passwords, or bank details."},

             {"how can hackers use social media to attack people","They gather personal information for scams or phishing."},

             {"what are privacy settings and why are they important","They control who can see your information online."},

             {"how can i protect my identity online","Use strong passwords and limit personal information sharing."},



    // Device Security

             {"how can i secure my home wifi network","Use a strong password and change the router default password."},

             {"why should i update my software regularly","Updates fix security vulnerabilities."},

             {"what is a firewall","A firewall monitors network traffic and blocks threats."},

             {"how do i know if my device has been hacked","Signs include slow performance, unknown apps, or strange pop-ups."},

             {"what should i do if my phone is stolen","Lock the device remotely and report it to your service provider."},

             {"how can i protect my laptop when using it in public","Avoid public Wi-Fi and lock your device when not using it."},



    // Best Practices

             {"what are the best cybersecurity habits everyone should follow","Use strong passwords, update software, and avoid suspicious links."},

             {"what should i do if i become a victim of a cyberattack","Change passwords, scan devices, and report the incident."},

             {"what is identity theft","Identity theft is when someone uses your personal information without permission."},

             {"how can i avoid identity theft","Protect your personal information and use strong passwords."},

             {"what is a secure website","A secure website uses HTTPS encryption."},

             {"what does https mean","HTTPS means HyperText Transfer Protocol Secure."},

             {"what should i do if i click a suspicious link","Close it and run antivirus software."},

             {"what is social engineering","A technique used to manipulate people into revealing confidential information."},

             {"why should i lock my computer","It prevents unauthorized access to your data."},

             {"what is a vpn","A VPN protects your internet connection and privacy."},

             {"is it safe to download free software","Only download from trusted websites."},

             {"what is a data breach","A data breach happens when sensitive information is stolen."},

             {"how do i know if a website is fake","Check the URL, spelling, and HTTPS."},

             {"what is encryption","Encryption converts data into secure code."},

             {"why is cybersecurity awareness important","It helps people recognize cyber threats."},

             {"what is spyware","Spyware secretly collects information without permission."},

             {"why should i back up my data","Backups protect files from loss or malware."},

             {"what is a cyber threat","Any activity that can harm systems or data."},

             {"how can i protect my wifi network","Use a strong password and encryption."},

             {"what is cybercrime","Illegal activity done using computers or the internet."},

             {"how can i stay safe when shopping online","Use trusted websites and secure payment methods."}
};

        Dictionary<string, string> defination = new Dictionary<string, string>()
            {
                 {"my name","Your name is " + name + ". I've never heard that name before but it's abolutely unique!"},

                  { "cybersecurity", "Cybersecurity is the practice of protecting systems, networks, and data from digital attacks." },
            
            // Password-related terms
                { "password", "A password is a secret word or phrase used to access an account or system." },

                { "strong password", "Use at least 12 characters including letters, numbers, and symbols." },

                { "weak password", "Short passwords, common words, or personal information make passwords weak." },

                { "password manager", "A password manager securely stores and generates strong passwords." },
            
            // Threat types
                { "phishing", "Phishing is a scam where attackers trick users into giving sensitive information." },

                { "malware", "Malware is harmful software designed to damage or access systems without permission." },

                { "ransomware", "Ransomware is malware that locks files and demands payment to unlock them." },

                { "virus", "A virus is a type of malware that spreads by attaching itself to files." },

                { "worm", "A worm is malware that spreads automatically through networks." },

                { "trojan", "A Trojan is malware disguised as legitimate software." },

                { "spyware", "Spyware is malware that secretly collects user information." },

                { "adware", "Adware is software that displays unwanted advertisements." },

                { "keylogger", "A keylogger records keystrokes to capture sensitive information." },

                { "botnet", "A botnet is a network of infected computers controlled by an attacker." },

                { "ddos attack", "A DDoS attack overwhelms a system with traffic to make it unavailable." },
            
            // Security measures
           
                { "firewall", "A firewall is a system that blocks unauthorized access to or from a network." },

                { "encryption", "Encryption is the process of converting data into a secure code." },

                { "decryption", "Decryption is converting encrypted data back into readable form." },

                { "antivirus", "Antivirus software detects and removes malicious programs." },

                { "vpn", "A VPN is a secure connection that protects your internet activity." },

                { "backup", "A backup is a copy of data stored for recovery." },

                { "software update", "A software update fixes bugs and security vulnerabilities." },
            
            // Authentication & Authorization
           
                { "authentication", "Authentication is the process of verifying a user's identity." },

                { "authorization", "Authorization determines what a user is allowed to access." },

                { "multi factor authentication", "It is a security process that requires multiple methods to verify identity." },

                { "two factor authentication", "It is a type of authentication using two verification methods." },
            
            // Security incidents
           
                { "data breach", "A data breach is when sensitive information is accessed without permission." },

                { "cyberattack", "A cyberattack is an attempt to damage or access systems illegally." },

                { "cyber threat", "A cyber threat is a potential danger to systems or data." },

                { "identity theft", "Identity theft is stealing personal information for fraud." },

                { "spam", "Spam is unwanted or harmful messages sent online." },

                { "scam", "A scam is a fraudulent scheme to steal money or information." },

                { "cybercrime", "Cybercrime is illegal activity done using computers or the internet." },
            
           
                // People
           
                { "hacker", "A hacker is a person who gains unauthorized access to systems." },

                { "ethical hacking", "Ethical hacking is legal hacking used to find and fix security weaknesses." },
            
            // Web security
           
                { "https", "HTTPS is a secure version of HTTP that encrypts web traffic." },

                { "http", "HTTP is a protocol used to transfer data over the internet." },

                { "secure website", "A secure website uses encryption to protect user data." },
            
            // Social engineering
           
                { "social engineering", "Social engineering tricks people into revealing confidential information." },
            
            // Network concepts
           
                
               { "public wi-fi", "Public Wi-Fi is a shared internet connection that may be insecure." },

                { "private network", "A private network is restricted and secure from public access." },

                { "router", "A router directs internet traffic between devices." },

                { "ip address", "An IP address identifies a device on a network." },

                { "domain name", "A domain name is the address of a website." },

                { "network security", "Network security protects computer networks from threats." },
            
            // Security domains
           
                { "cloud security", "Cloud security protects data stored online." },

                { "information security", "Information security protects all forms of data." },
            
            // Privacy & Digital footprint
           
                { "digital footprint", "A digital footprint is the data you leave behind online." },

                { "privacy", "Privacy is the protection of personal information." },
            
            // Organizational security
          
                { "security policy", "A security policy is a set of rules to protect systems and data." },

                { "incident response", "Incident response is how organizations handle cyberattacks." },

                { "risk management", "Risk management is identifying and reducing cybersecurity risks." }

            };
        Dictionary<string, string> explaination = new Dictionary<string, string>()
             {

                { "cybersecurity", "The practice of protecting systems, networks, and data from digital attacks. Tip 1: Always keep software updated. Tip 2: Use unique passwords for each account. Tip 3: Enable automatic security patches." },

                { "password", "A secret word or phrase used to authenticate access to a system. Tip 1: Make passwords at least 12 characters long. Tip 2: Avoid dictionary words or personal info. Tip 3: Never share passwords via email or text." },

                { "phishing", "Fraudulent attempts to trick you into revealing sensitive info via fake emails/websites. Tip 1: Hover over links before clicking. Tip 2: Check sender email addresses carefully. Tip 3: Never enter credentials on pop-up windows." },

                { "malware", "Malicious software designed to damage or exploit devices. Tip 1: Run regular antivirus scans. Tip 2: Don't download from untrusted sites. Tip 3: Disable auto-run for USB drives." },

                { "ransomware", "Malware that encrypts your files and demands payment for decryption. Tip 1: Maintain offline backups. Tip 2: Never pay the ransom (no guarantee). Tip 3: Use application whitelisting." },

                { "virus", "Self-replicating malware that attaches to clean files and spreads. Tip 1: Scan email attachments before opening. Tip 2: Keep your OS updated. Tip 3: Use real-time antivirus protection." },

                { "worm", "Standalone malware that replicates across networks without human action. Tip 1: Disable unnecessary network services. Tip 2: Use network segmentation. Tip 3: Apply security patches immediately." },

                { "trojan", "Malware disguised as legitimate software to trick users. Tip 1: Only download from official sources. Tip 2: Verify digital signatures. Tip 3: Use application control tools." },

                { "firewall", "Network security system that monitors and controls incoming/outgoing traffic. Tip 1: Keep firewall enabled at all times. Tip 2: Configure outbound rules carefully. Tip 3: Regularly review firewall logs." },

                { "encryption", "Converting data into unreadable code to protect confidentiality. Tip 1: Encrypt sensitive files and emails. Tip 2: Use full-disk encryption on laptops. Tip 3: Never lose encryption keys." },

                { "decryption", "The process of converting encrypted data back to readable form. Tip 1: Store decryption keys securely offline. Tip 2: Use key escrow for business. Tip 3: Test decryption regularly on backups." },

                { "authentication", "Verifying a user's identity before granting access. Tip 1: Use MFA everywhere possible. Tip 2: Avoid SMS-based codes when possible. Tip 3: Implement biometrics as a factor." },

                { "authorization", "Determining what resources a verified user can access. Tip 1: Follow least privilege principle. Tip 2: Review permissions quarterly. Tip 3: Use role-based access control (RBAC)." },

                { "multi factor authentication", "Using two or more verification methods (something you know/have/are). Tip 1: Prefer authenticator apps over SMS. Tip 2: Enroll backup codes. Tip 3: Enable for email and banking first." },


               { "two factor authentication", "A subset of MFA using exactly two verification factors. Tip 1: Use hardware tokens for high-value accounts. Tip 2: Never approve unexpected push notifications. Tip 3: Keep recovery keys safe." },

                { "data breach", "Unauthorized exposure of confidential information. Tip 1: Monitor credit reports regularly. Tip 2: Use breach notification services. Tip 3: Change passwords immediately after known breaches." },

                { "hacker", "Someone who exploits system vulnerabilities (ethically or maliciously). Tip 1: Learn hacking to defend better. Tip 2: Never retaliate against attackers. Tip 3: Report vulnerabilities responsibly." },

                { "ethical hacking", "Authorized hacking to find and fix security flaws. Tip 1: Always get written permission. Tip 2: Define scope clearly. Tip 3: Document all findings thoroughly." },

                { "spyware", "Software that secretly monitors user activity and steals data. Tip 1: Avoid clicking suspicious ads. Tip 2: Run anti-spyware tools weekly. Tip 3: Check browser extensions regularly." },

                { "adware", "Software that automatically displays unwanted advertisements. Tip 1: Decline 'free' software toolbars. Tip 2: Use ad-blockers cautiously. Tip 3: Uninstall unknown browser add-ons." },

                { "keylogger", "Malware that records every keystroke typed on a device. Tip 1: Use on-screen keyboard for passwords. Tip 2: Keep antivirus active. Tip 3: Inspect USB ports for hardware keyloggers." },

                { "botnet", "Network of infected devices controlled remotely by attackers. Tip 1: Secure IoT devices with strong passwords. Tip 2: Monitor unusual outbound traffic. Tip 3: Disable telnet and unused ports." },

                { "ddos attack", "Overwhelming a server with traffic from multiple sources to cause outage. Tip 1: Use DDoS protection services. Tip 2: Scale bandwidth dynamically. Tip 3: Implement rate limiting." },

                { "vpn", "Encrypts internet traffic and hides your IP address for privacy. Tip 1: Choose no-log VPN providers. Tip 2: Enable kill switch feature. Tip 3: Avoid free VPNs (they sell data)." },

                { "https", "Secure HTTP with encryption via SSL/TLS certificates. Tip 1: Never enter passwords on HTTP sites. Tip 2: Look for padlock icon in address bar. Tip 3: Install HTTPS Everywhere extension." },

                { "http", "Unencrypted web protocol vulnerable to eavesdropping. Tip 1: Assume all HTTP traffic is public. Tip 2: Upgrade any HTTP site you run to HTTPS. Tip 3: Avoid logins on HTTP pages." },

                { "secure website", "Website using HTTPS and valid SSL/TLS certificates. Tip 1: Verify certificate details by clicking padlock. Tip 2: Check for EV certificates on banking sites. Tip 3: Don't ignore 'Not Secure' warnings." },

                { "identity theft", "Using someone's personal information fraudulently. Tip 1: Freeze credit reports. Tip 2: Shred financial documents. Tip 3: File taxes early to prevent refund fraud." },

                { "social engineering", "Psychological manipulation to trick users into revealing data. Tip 1: Verify urgent requests via separate channel. Tip 2: Never share OTPs with 'support'. Tip 3: Create a family 'safe word'." },

                { "cyberattack", "Deliberate exploitation of systems for malicious purposes. Tip 1: Assume breach mindset. Tip 2: Practice incident response drills. Tip 3: Keep offline backups." },

                { "cyber threat", "Potential danger to systems, networks, or data. Tip 1: Subscribe to threat intelligence feeds. Tip 2: Perform regular risk assessments. Tip 3: Update threat models quarterly." },

                { "antivirus", "Software that detects and removes malicious programs. Tip 1: Keep virus definitions updated daily. Tip 2: Run full scans weekly. Tip 3: Don't run two antivirus programs together." },

                { "software update", "Patches that fix security vulnerabilities and bugs. Tip 1: Enable automatic updates. Tip 2: Don't postpone for more than 7 days. Tip 3: Verify updates come from official sources." },

                { "backup", "Copy of data used for recovery after loss or ransomware. Tip 1: Follow 3-2-1 rule (3 copies, 2 media, 1 offsite). Tip 2: Test restores quarterly. Tip 3: Keep backups disconnected when not used." },

                { "cloud security", "Protecting data, apps, and infrastructure in cloud environments. Tip 1: Never hardcode cloud keys in code. Tip 2: Enable cloud audit logging. Tip 3: Use cloud-native security tools." },

                { "network security", "Policies and tools to protect network integrity and usability. Tip 1: Segment IoT devices from main network. Tip 2: Disable unused ports and services. Tip 3: Monitor for rogue access points." },

                { "information security", "Preserving confidentiality, integrity, and availability of data. Tip 1: Classify data by sensitivity. Tip 2: Implement data loss prevention (DLP). Tip 3: Train employees on handling categories." },

                { "public wi-fi", "Unencrypted networks in public places, highly vulnerable. Tip 1: Always use VPN on public Wi-Fi. Tip 2: Disable auto-connect and file sharing. Tip 3: Forget network after use." },

                { "private network", "Restricted network with controlled access and encryption. Tip 1: Use WPA3 encryption for Wi-Fi. Tip 2: Change default router passwords. Tip 3: Disable WPS feature." },

                { "router", "Device that directs network traffic between devices and internet. Tip 1: Update router firmware regularly. Tip 2: Disable remote administration. Tip 3: Change default admin credentials." },

                { "ip address", "Unique identifier for devices on a network. Tip 1: Use VPN to hide your IP. Tip 2: Never share IP addresses publicly. Tip 3: Use dynamic IPs for home networks." },

                { "domain name", "Human-readable web address mapped to an IP address. Tip 1: Check domain spelling for typosquatting. Tip 2: Verify WHOIS info before trusting. Tip 3: Use domain reputation checkers." },

                { "spam", "Unsolicited bulk messages, often containing scams or malware. Tip 1: Never unsubscribe from suspicious spam. Tip 2: Use email filtering. Tip 3: Report spam to your provider." },

                { "scam", "Fraudulent scheme to steal money or information. Tip 1: 'If it's too good to be true, it is.' Tip 2: Never pay with gift cards. Tip 3: Verify charities before donating." },

                { "cybercrime", "Illegal activities conducted via computers or networks. Tip 1: Report incidents to law enforcement. Tip 2: Preserve evidence (logs, screenshots). Tip 3: Know local cybercrime reporting channels." },

                { "digital footprint", "Trail of data you leave online (posts, cookies, purchases). Tip 1: Google yourself regularly. Tip 2: Limit social media visibility. Tip 3: Delete old unused accounts." },

                { "privacy", "Control over how personal information is collected and used. Tip 1: Opt out of data brokers. Tip 2: Use privacy-focused browsers. Tip 3: Adjust app permission settings." },

                { "security policy", "Documented rules for protecting organizational assets. Tip 1: Make policies readable (not legalese). Tip 2: Enforce with technical controls. Tip 3: Review annually." },

                { "incident response", "Structured process for handling security breaches. Tip 1: Create a written IR plan. Tip 2: Practice tabletop exercises. Tip 3: Document lessons learned after incidents." },

                { "risk management", "Identifying, assessing, and mitigating security risks. Tip 1: Prioritize risks by impact/likelihood. Tip 2: Accept, transfer, mitigate, or avoid. Tip 3: Review risks quarterly." },

                { "strong password", "Complex password resistant to guessing/cracking (length > randomness). Tip 1: Use 4+ random words (e.g., correct-horse-battery). Tip 2: Minimum 15 characters. Tip 3: Never reuse across sites." },

                { "password manager", "Software that generates and stores complex passwords securely. Tip 1: Use master password that's very strong. Tip 2: Enable MFA on the manager. Tip 3: Choose offline or zero-knowledge options." },

                { "weak password", "Easily guessed password (short, common, personal). Tip 1: Avoid 'password123', 'qwerty', birthdays. Tip 2: Never use 'admin' or 'password'. Tip 3: Check if your password appears in breach lists (haveibeenpwned)." }
             };
        Dictionary<string, string> Answers = new Dictionary<string, string>()
            {
                {"Question1B","Authentication = Proving who you are (Identity Verification)\r\n\r\nExample: Entering your username and password\r\n\r\nAnswers the question: \"Are you who you claim to be?\"\r\n\r\nAuthorization = Determining what you can do (Access Control)\r\n\r\nExample: Can you view payroll data? Can you delete files?\r\n\r\nAnswers the question: \"What are you allowed to do?\""},
                {"Question2B","This is a 33-character passphrase with:\r\n\r\nHigh entropy (randomness)\r\n\r\nMix of dictionary words + numbers\r\n\r\nLength makes brute-force attacks practically impossible\r\n\r\nEven if attackers use dictionary attacks, the length (33 chars) creates massive complexity"},
                {"Question3C","This follows the \"Verify Independently\" principle:\r\n\r\nHover - Reveals the actual destination URL (may be different from displayed text)\r\n\r\nCheck URL - Look for subtle spoofing (e.g., \"secure-login.com\" vs \"secure-logln.com\")\r\n\r\nCall directly - Use the number from your bank's official website or back of your card"},
                {"Question4C","Ransom + Software = Ransomware\r\n\r\nEncrypts files using strong encryption (AES-256)\r\n\r\nDemands payment (usually cryptocurrency) for decryption key\r\n\r\nExamples: WannaCry, LockBit, Ryuk"},
                {"Question5C","HTTPS = HTTP + SSL/TLS encryption\r\n\r\nEncrypts ALL data between browser and website\r\n\r\nPrevents: Eavesdropping, Man-in-the-Middle attacks, Data tampering\r\n\r\nUses certificates to verify website identity"},
                {"Question6C","SMS has multiple critical vulnerabilities:\r\n\r\nSIM-Swapping Attacks\r\n\r\nAttacker convinces carrier to transfer your number to their SIM\r\n\r\nReceives all your 2FA codes\r\n\r\nSS7 Protocol Vulnerabilities\r\n\r\nOld telephone network protocol\r\n\r\nCan intercept SMS globally\r\n\r\nPhone Number Portability\r\n\r\nNumbers can be ported to other carriers\r\n\r\nInterception\r\n\r\nSMS not encrypted end-to-end"},
                {"Question7B","Vishing = Voice + Phishing\r\n\r\nAttackers use phone calls (not email or SMS)\r\n\r\nExploits trust in human voice\r\n\r\nCreates urgency to bypass critical thinking"},
                {"Question8B","The 3-2-1 rule is the gold standard for data protection:\r\n\r\n3 = Three copies total\r\n\r\nPrimary copy (original data)\r\n\r\nBackup Copy 1\r\n\r\nBackup Copy 2\r\n\r\n2 = Two different media types\r\n\r\nExample: Hard drive + Cloud Storage\r\n\r\nOr: NAS + Tape Backup\r\n\r\nOr: SSD + External HDD\r\n\r\nProtects against media failure\r\n\r\n1 = One offsite copy\r\n\r\nPhysically different location\r\n\r\nProtects against: Fire, Flood, Theft, Natural disasters\r\n\r\nCloud storage is considered offsite\r\n\r\nExample Implementation:\r\nOriginal: Data on your laptop (Copy 1)\r\n\r\nMedia 1: External hard drive backup (Copy 2)\r\n\r\nMedia 2 + Offsite: Cloud backup (Copy 3)"},
                {"Question9B","DDoS = Distributed Denial of Service\r\n\r\nHow It Works:\r\n\r\nAttacker builds a botnet (network of compromised devices)\r\n\r\nBotnet sends massive traffic to target\r\n\r\nTarget server becomes overloaded\r\n\r\nLegitimate users can't access service\r\n\r\nKey Characteristics:\r\nDistributed: Uses many sources (geographically spread)\r\n\r\nVolumetric: Floods with traffic\r\n\r\nGoal: Make service unavailable, NOT steal data"},
                {"Question10B","Password managers solve two critical problems:\r\n\r\n1. Password Reuse Problem\r\n\r\nMost people reuse passwords across sites\r\n\r\nOne breach = ALL accounts compromised\r\n\r\nPassword manager creates unique password for EACH site\r\n\r\n2. Weak Password Problem\r\n\r\nHumans create weak, predictable passwords\r\n\r\nPassword manager generates truly random passwords\r\n\r\nCan create 20+ character passwords easily"},
                {"Question11C","Trojan = Deceives users into installing\r\n\r\nNamed after the mythological Trojan Horse\r\n\r\nDisguised as: Free games, Antivirus, Software updates, PDF files\r\n\r\nHow Trojans Work:\r\n\r\nUser thinks they're installing legitimate software\r\n\r\nTrojan installs secretly\r\n\r\nPerforms malicious actions:\r\n\r\nSteals data\r\n\r\nInstalls backdoor\r\n\r\nDownloads additional malware"},
                {"Question12B","Explanation: The correct first step is to CONTAIN the breach and PRESERVE evidence. Notification comes later after understanding the scope. Informing customers prematurely without proper investigation can cause unnecessary panic and legal issues."},
                {"Question13C","Your digital footprint includes:\r\n\r\nActive Footprint (You knowingly share):\r\n\r\nSocial media posts\r\n\r\nComments and reviews\r\n\r\nPhotos and videos\r\n\r\nOnline purchases\r\n\r\nWebsite registrations\r\n\r\nPassive Footprint (Collected without your direct input):\r\n\r\nCookies (tracks browsing behavior)\r\n\r\nIP address\r\n\r\nDevice fingerprint\r\n\r\nLocation data\r\n\r\nSearch history\r\n\r\nShopping patterns"},
                {"Question14A","Explanation: Attackers use urgency (e.g., \"Your account will be closed in 1 hour!\") to bypass critical thinking. This pressure tactic prevents victims from verifying the request through proper channels."},
                {"Question15C","Encryption = Transforming plaintext → ciphertext\r\n\r\nPurpose: Maintain CONFIDENTIALITY\r\n\r\nHow It Works:\r\n\r\nPlaintext (readable) + Encryption Algorithm + Key\r\n\r\nOutput = Ciphertext (unreadable)\r\n\r\nOnly authorized parties with correct key can decrypt\r\n\r\nReal-World Example:\r\n\r\nPlaintext: \"My password is Secret123\"\r\n\r\nCiphertext: \"x8f7g9h2j4k6l8m1n3p5q7r9s2t4u6v8w\"\r\n\r\nWithout key: Completely unreadable"},
                {"Question16B","Explanation: A firewall's primary function is to monitor and control network traffic based on security rules. It filters traffic based on IP, port, and protocol to protect against unauthorized access. It does NOT speed up connections."},
                {"Question17B","Botnet = Bot + Network\r\n\r\nHow It Works:\r\n\r\nMalware infects devices (computers, IoT, phones)\r\n\r\nDevices become \"zombies\" or \"bots\"\r\n\r\nAttacker controls via Command & Control (C&C) server\r\n\r\nBots receive instructions to perform attacks\r\n\r\nCommon Uses of Botnets:\r\n\r\nDDoS attacks\r\n\r\nSpam emails\r\n\r\nCredential stuffing\r\n\r\nCryptocurrency mining\r\n\r\nClick fraud\r\n\r\nData theft"},

            };

        private string connectionString =
                      "server=localhost;" +
                      "port=3307;" +
                      "database=taskstorage;" +
                      "uid=root;" +
                      "pwd=Sandilenhlapo8@;";


        public MainWindow()
        {
            InitializeComponent();
            myArt.Text = art;
            //calling the voice method when the content is rendered
            this.ContentRendered += voiceCalling;


        }
        //method to call the voice when the content is rendered
        private void voiceCalling(object sender, EventArgs e)
        {
            //creating an instance of the speech synthesizer class to use the speak method
            SpeechSynthesizer ChatbotVoice = new SpeechSynthesizer();
            ChatbotVoice.Speak("hello welcome to the cybersecurity awareness bot.");
            ChatbotVoice.Speak("I'm here to help you to stay safe online. what is your name?");
        }
        //method to send the request when the user clicks the send button
        public void sendrequest(object sender, RoutedEventArgs e)
        {
            //creating an instance of the speech synthesizer class to use the speak method

            SpeechSynthesizer voice = new SpeechSynthesizer();
            Border TextAppearence = new Border();
            TextBlock input = new TextBlock();
            StackPanel stackPanel = new StackPanel();

            //taking the user input and converting it to lowercase and trimming the spaces
            input.Text = message.Text.ToLower().Trim();


            //setting the properties of the user input textblock and the border around it to make it look like a chat bubble
            input.HorizontalAlignment = HorizontalAlignment.Right;
            input.FontSize = 16;
            input.Background = Brushes.Transparent;
            input.Foreground = Brushes.Blue;
            input.Width = 400;

            //setting the properties of the border around the user input to make it look like a chat bubble
            TextAppearence.Background = Brushes.SkyBlue;
            TextAppearence.CornerRadius = new CornerRadius(7);
            TextAppearence.Width = 400;
            TextAppearence.Height = Double.NaN;
            TextAppearence.HorizontalAlignment = HorizontalAlignment.Right;
            TextAppearence.BorderBrush = new SolidColorBrush(Colors.Transparent);
            TextAppearence.BorderBrush = new SolidColorBrush(Colors.Transparent);
            TextAppearence.Child = input;

            //adding the user input to the stack panel to display it in the chat window
            displaying.Children.Add(TextAppearence);

            TaskGrid.Visibility = Visibility.Collapsed;

            Border ChatBotTextAppearence = new Border();
            TextBlock output = new TextBlock();
            //checking the user input for specific keywords and responding accordingly
            if (input.Text.Contains("my name is"))
            {
                //taking the name from the user input and converting it to uppercase
                name = input.Text.Substring(11).ToUpper();
                output.Text = "CHATBOT \n" + "Nice to meet you " + name
                              + ".\n How can I help you " + name;
            }
            //checking if the user input contains bye or goodbye and responding accordingly
            else if (input.Text.Contains("bye") || input.Text.Contains("goodbye"))
            {
                MessageBox.Show("Goodbye " + name + " it was nice talking to you.");
                Environment.Exit(0);
            }
            // adding the tast
            else if (input.Text.Contains("add") && input.Text.Contains("task"))
            {
                UserTible = input.Text.Replace("add", "").Replace("task", "").Trim();
                output.Text = "CHATBOT \n" + "Task added with a Description :" + RespondSystem(input.Text, name) + ". Would you like a reminder?";
                UserDescriprion = RespondSystem(input.Text, name);
            }

            //activity log
            else if (input.Text.Contains("show activity log") || input.Text.Contains("what have you done for me?") || input.Text.Contains("activity log"))
            {

                LoadTask();

            }
            //delate task
            else if (input.Text.Contains("delete"))
            {
                LoadTask();
                output.Text = "please enter TackID to delate a task. \n" +
                    "eg: TaskID=1";
            }
            else if (input.Text.Contains("taskid"))
            {
                using (MySqlConnection connection =
                        new MySqlConnection(connectionString))
                {
                    connection.Open();

                    string query =
                        "DELETE FROM taskstorage.tacks WHERE TaskID=@TaskID;";

                    MySqlCommand command =
                        new MySqlCommand(query, connection);

                    command.Parameters.AddWithValue("@TaskID", new string(input.Text.Where(char.IsDigit).ToArray()));

                    command.ExecuteNonQuery();
                    output.Text = "Task deleted successfully.";
                }

            }
            //set a reminder for a task
            else if (input.Text.Contains("remind me in") || input.Text.Contains("remind") || input.Text.Contains("tomorrow"))
            {
                string date = string.Join(" ", input.Text.Replace("yes", "").Replace(",", "").Split(' ').Reverse().Take(2).Reverse());
                output.Text = date;
                //LET ME REMIND YOU
                string numberString = "";

                if (input.Text.Contains("tomorrow"))
                {
                    numberString = "1";
                }
                else
                {
                    numberString = Regex.Match(date, @"\d+").Value;
                }



                DateTime TaskDue = DateTime.Now.AddDays(Convert.ToDouble(numberString));

                using (MySqlConnection connection =
                        new MySqlConnection(connectionString))
                {
                    connection.Open();

                    string query =
                        "INSERT INTO Tacks(Title, Description,OptionalReminder,DueDate) VALUES(@title,@description,@optionalReminder,@dueDate)";

                    MySqlCommand command =
                        new MySqlCommand(query, connection);

                    command.Parameters.AddWithValue("@title", UserTible);
                    command.Parameters.AddWithValue("@description", UserDescriprion);
                    command.Parameters.AddWithValue("@optionalReminder", date.Replace("in", ""));
                    command.Parameters.AddWithValue("@dueDate", TaskDue);

                    command.ExecuteNonQuery();
                    output.Text = "Got it, I'll remind you in " + date.Replace("in", "");
                }

                MessageBox.Show("Task Saved");
                LoadTask();
                TaskGrid.Visibility = Visibility.Visible;
            }
            //checking if the user input contains summary or summarize or preview or review or overview or previous conversation or remind me and responding accordingly
            else if (input.Text.Contains("summary") || input.Text.Contains("summarize") || input.Text.Contains("preview") || input.Text.Contains("overview") || input.Text.Contains("show more"))
            {
                output.Text = "CHATBOT \n" + Summary();
            }
            //checking if the user input contains tell me more or give me more or explain and responding accordingly
            else if (input.Text.Contains("tell me more") || input.Text.Contains("give me more") || input.Text.Contains("explain"))
            {
                output.Text = "CHATBOT \n" + Explaination(Info);
                conversationHistory.Add(output.Text);
            }
            //checking if the user input contains what is and responding accordingly
            else if (input.Text.Contains("what is"))
            {
                output.Text = "CHATBOT \n" + Defination(input.Text, name);
                conversationHistory.Add(output.Text);
            }
            //checking if the user input contains tip or tips and responding accordingly
            else if (input.Text.Contains("tip") || input.Text.Contains("tips"))
            {
                output.Text = "CHATBOT \n" + TipsMethod(input.Text);
                conversationHistory.Add(output.Text);

            }
            //quiz completed
            else if (input.Text.Contains("quiz") || input.Text.Contains("quizzes"))
            {

                QuizsDisplay.Visibility = Visibility.Visible;
                displaying.Visibility = Visibility.Collapsed;
                output.Text = "Quiz completed!";
                conversationHistory.Add(output.Text);

                using (MySqlConnection connection =
                        new MySqlConnection(connectionString))
                {
                    connection.Open();

                    string query =
                        "INSERT INTO Tacks(Title, Description,OptionalReminder,DueDate) VALUES(@title,@description,@optionalReminder,@dueDate)";

                    MySqlCommand command =
                        new MySqlCommand(query, connection);

                    command.Parameters.AddWithValue("@title", "quizzes");
                    command.Parameters.AddWithValue("@description", "17 quizzes completed!");
                    command.Parameters.AddWithValue("@optionalReminder", "2 hours");
                    command.Parameters.AddWithValue("@dueDate", DateTime.Now);

                    command.ExecuteNonQuery();

                }
            }
            //if the user input does not contain any of the above keywords then it will respond with the general response method
            else
            {
                output.Text = "CHATBOT \n" + GenerateNLPResponse(input.Text);
                conversationHistory.Add(output.Text);
            }



            output.HorizontalAlignment = HorizontalAlignment.Left;
            output.FontSize = 16;
            output.Background = Brushes.Transparent;
            output.Foreground = Brushes.White;
            output.Width = 400;
            output.Height = Double.NaN;
            output.TextWrapping = TextWrapping.Wrap;
            output.MinHeight = 30;
            output.MaxHeight = 400;

            ChatBotTextAppearence.Background = Brushes.Black;
            ChatBotTextAppearence.CornerRadius = new CornerRadius(7);
            ChatBotTextAppearence.Width = 410;
            ChatBotTextAppearence.Height = Double.NaN;
            ChatBotTextAppearence.HorizontalAlignment = HorizontalAlignment.Left;
            ChatBotTextAppearence.BorderThickness = new Thickness(7);
            ChatBotTextAppearence.BorderBrush = new SolidColorBrush(Colors.Transparent);
            ChatBotTextAppearence.Child = output;



            displaying.Children.Add(ChatBotTextAppearence);

            message.Text = "";


        }
        private void LoadTask()
        {
            List<Tacks> theTasks = new List<Tacks>();

            using (MySqlConnection connection =
                new MySqlConnection(connectionString))
            {
                connection.Open();

                string query = "SELECT * FROM Tacks";

                MySqlCommand command =
                    new MySqlCommand(query, connection);

                MySqlDataReader reader =
                    command.ExecuteReader();

                while (reader.Read())
                {
                    theTasks.Add(new Tacks
                    {
                        TaskID = Convert.ToInt32(reader["TaskID"]),
                        Title = reader["Title"].ToString(),
                        Description = reader["Description"].ToString(),
                        OptionalReminder = reader["OptionalReminder"].ToString(),
                        ReminderDay = Convert.ToDateTime(reader["ReminderDay"]),
                        DueDate = Convert.ToDateTime(reader["DueDate"])
                    });
                }
            }
            TextBox chatbotname = new TextBox();
            chatbotname.Foreground = new SolidColorBrush(Colors.White);
            chatbotname.Background = new SolidColorBrush(Colors.Transparent);
            chatbotname.BorderBrush = new SolidColorBrush(Colors.Transparent);
            chatbotname.Text = "CHATBOT:\n those are the tasks you have:";

            displaying.Children.Add(chatbotname);
            foreach (var task in theTasks)
            {
                TextBox logs = new TextBox();
                logs.Foreground = new SolidColorBrush(Colors.White);
                logs.Background = new SolidColorBrush(Colors.Transparent);
                logs.BorderBrush = new SolidColorBrush(Colors.Transparent);
                logs.Text = $"TaskID: {task.TaskID}        Title: {task.Title}          DueDate: {task.DueDate}";
                displaying.Children.Add(logs);
            }
            TaskGrid.ItemsSource = theTasks;

        }

        public string Summary()
        {
            string summary = "";
            int num = 1;
            foreach (string conversation in conversationHistory)
            {
                summary += num + " " + conversation.Replace("CHATBOT", "") + "\n";
                num = num + 1;
            }
            LoadTask();
            return "\n LET ME REMIND YOU WHAT WE ALSO DID DUING OUR CONVERSATION" + name + ":\n" + summary;

        }

        static string RespondSystem(string interactor, string name)
        {
            string respond = " ";




            foreach (string keyword in memory.Keys)
            {
                if (interactor.Contains(keyword))
                {
                    respond = memory[keyword];
                    Info = keyword;
                    break;
                }
                else
                {
                    respond = "Sorry I don't understand that. Please ask me a general cybersecurity question or how to stay safe online.";
                }
            }

            return respond;
        }
        public string TipsMethod(string Tips)
        {
            string TipRespond = "";

            foreach (string keyword in TipsMemory.Keys)
            {
                if (Tips.Contains(keyword))
                {
                    TipRespond = TipsMemory[keyword];
                    Info = keyword;
                    break;
                }
                else
                {
                    TipRespond = "Sorry I don't understand that. Please ask me a general cybersecurity question or how to stay safe online.";
                }
            }

            return TipRespond;
        }
        public string Defination(string question, string Name)
        {
            string answered = "";


            foreach (string keyword in defination.Keys)
            {
                if (question.Contains(keyword))
                {
                    answered = defination[keyword];
                    Info = keyword;
                    break;
                }
                else
                {
                    answered = "Sorry I don't understand that. Please ask me a general cybersecurity question or how to stay safe online.";
                }
            }

            return answered;
        }
        public string Explaination(string info)
        {
            string explain = "";



            foreach (string keyword in explaination.Keys)
            {
                if (info.Contains(keyword))
                {
                    explain = explaination[keyword];
                    Info = keyword;
                    break;
                }
                else
                {
                    explain = "Sorry I don't understand that. Please ask me a general cybersecurity question or how to stay safe online.";
                }
            }

            return explain;
        }
        private void RadioButton_Checked(object sender, RoutedEventArgs e)
        {
            if (counter == 1)
            {
                if (Question1B.IsChecked == true)
                {
                    MessageBox.Show("correct answer");
                    QuizzesAnswers();
                    TotalMarks = ++TotalMarks;
                }
                else
                {
                    MessageBox.Show("Incorrect answer. B is the correct answer.Let me explain:");
                    QuizzesAnswers();
                }
                Question1.Visibility = Visibility.Collapsed;
                Question2.Visibility = Visibility.Visible;
            }
            else if (counter == 2)
            {
                if (Question2B.IsChecked == true)
                {
                    MessageBox.Show("Great job!");
                    QuizzesAnswers();
                    TotalMarks = ++TotalMarks;
                }
                else
                {
                    MessageBox.Show("Incorrect answer. B is the correct answer.Let me explain:");
                    QuizzesAnswers();
                }
                Question2.Visibility = Visibility.Collapsed;
                Question3.Visibility = Visibility.Visible;
            }
            else if (counter == 3)
            {
                if (Question3C.IsChecked == true)
                {
                    MessageBox.Show("You're a cybersecurity pro!");
                    QuizzesAnswers();
                    TotalMarks = ++TotalMarks;
                }
                else
                {
                    MessageBox.Show("Incorrect answer. C is the correct answer.Let me explain:");
                    QuizzesAnswers();
                }
                Question3.Visibility = Visibility.Collapsed;
                Question4.Visibility = Visibility.Visible;
            }
            else if (counter == 4)
            {
                if (Question4C.IsChecked == true)
                {
                    MessageBox.Show("correct answer");
                    QuizzesAnswers();
                    TotalMarks = ++TotalMarks;
                }
                else
                {
                    MessageBox.Show("Incorrect answer. Cis the correct answer.Let me explain:");
                    QuizzesAnswers();
                }
                Question4.Visibility = Visibility.Collapsed;
                Question5.Visibility = Visibility.Visible;
            }
            else if (counter == 5)
            {
                if (Question5C.IsChecked == true)
                {
                    MessageBox.Show("Great job!");
                    QuizzesAnswers();
                    TotalMarks = ++TotalMarks;
                }
                else
                {
                    MessageBox.Show("Incorrect answer. C is the correct answer.Let me explain:");
                    QuizzesAnswers();
                }
                Question5.Visibility = Visibility.Collapsed;
                Question6.Visibility = Visibility.Visible;
            }
            else if (counter == 6)
            {
                if (Question6C.IsChecked == true)
                {
                    MessageBox.Show("You're a cybersecurity pro!");
                    QuizzesAnswers();
                    TotalMarks = ++TotalMarks;
                }
                else
                {
                    MessageBox.Show("Incorrect answer. C is the correct answer.Let me explain:");
                    QuizzesAnswers();
                }
                Question6.Visibility = Visibility.Collapsed;
                Question7.Visibility = Visibility.Visible;
            }
            else if (counter == 7)
            {
                if (Question7B.IsChecked == true)
                {
                    MessageBox.Show("correct answer");
                    QuizzesAnswers();
                    TotalMarks = ++TotalMarks;

                }
                else
                {
                    MessageBox.Show("Incorrect answer. B is the correct answer.Let me explain:");
                    QuizzesAnswers();
                }
                Question7.Visibility = Visibility.Collapsed;
                Question8.Visibility = Visibility.Visible;
            }
            else if (counter == 8)
            {
                if (Question8B.IsChecked == true)
                {
                    MessageBox.Show("Great job!");
                    QuizzesAnswers();
                    TotalMarks = ++TotalMarks;
                }
                else
                {
                    MessageBox.Show("Incorrect answer. B is the correct answer.Let me explain:");
                    QuizzesAnswers();
                }
                Question8.Visibility = Visibility.Collapsed;
                Question9.Visibility = Visibility.Visible;

            }
            else if (counter == 9)
            {
                if (Question9B.IsChecked == true)
                {
                    MessageBox.Show("You're a cybersecurity pro!");
                    QuizzesAnswers();
                    TotalMarks = ++TotalMarks;
                }
                else
                {
                    MessageBox.Show("Incorrect answer. B is the correct answer.Let me explain:");
                    QuizzesAnswers();
                }
                Question9.Visibility = Visibility.Collapsed;
                Question10.Visibility = Visibility.Visible;
            }
            else if (counter == 10)
            {
                if (Question10B.IsChecked == true)
                {
                    MessageBox.Show("correct answer");
                    QuizzesAnswers();
                    TotalMarks = ++TotalMarks;
                }
                else
                {
                    MessageBox.Show("Incorrect answer. B is the correct answer.Let me explain:");
                    QuizzesAnswers();
                }
                Question10.Visibility = Visibility.Collapsed;
                Question11.Visibility = Visibility.Visible;
            }
            else if (counter == 11)
            {
                if (Question11C.IsChecked == true)
                {
                    MessageBox.Show("correct answer");
                    QuizzesAnswers();
                    TotalMarks = ++TotalMarks;
                }
                else
                {
                    MessageBox.Show("Incorrect answer. C is the correct answer.Let me explain:");
                    QuizzesAnswers();
                }
                Question11.Visibility = Visibility.Collapsed;
                Question12.Visibility = Visibility.Visible;
            }
            else if (counter == 12)
            {
                if (Question12B.IsChecked == true)
                {
                    MessageBox.Show("Great job!");
                    QuizzesAnswers();
                    TotalMarks = ++TotalMarks;
                }
                else
                {
                    MessageBox.Show("Incorrect answer. FALSE is the correct answer.Let me explain:");
                    QuizzesAnswers();
                }
                Question12.Visibility = Visibility.Collapsed;
                Question13.Visibility = Visibility.Visible;
            }
            else if (counter == 13)
            {
                if (Question13C.IsChecked == true)
                {
                    MessageBox.Show("correct answer");
                    QuizzesAnswers();
                    TotalMarks = ++TotalMarks;
                }
                else
                {
                    MessageBox.Show("Incorrect answer. C is the correct answer.Let me explain:");
                    QuizzesAnswers();
                }
                Question13.Visibility = Visibility.Collapsed;
                Question14.Visibility = Visibility.Visible;
            }
            else if (counter == 14)
            {
                if (Question14A.IsChecked == true)
                {
                    MessageBox.Show("You're a cybersecurity pro!");
                    QuizzesAnswers();
                    TotalMarks = ++TotalMarks;
                }
                else
                {
                    MessageBox.Show("Incorrect answer. TRUE is the correct answer.Let me explain:");
                    QuizzesAnswers();
                }
                Question14.Visibility = Visibility.Collapsed;
                Question15.Visibility = Visibility.Visible;
            }
            else if (counter == 15)
            {
                if (Question15C.IsChecked == true)
                {
                    MessageBox.Show("correct answer");
                    QuizzesAnswers();
                    TotalMarks = ++TotalMarks;

                }
                else
                {
                    MessageBox.Show("Incorrect answer. C is the correct answer.Let me explain:");
                    QuizzesAnswers();
                }
                Question15.Visibility = Visibility.Collapsed;
                Question16.Visibility = Visibility.Visible;
            }
            else if (counter == 16)
            {
                if (Question16B.IsChecked == true)
                {
                    MessageBox.Show("correct answer");
                    QuizzesAnswers();
                    TotalMarks = ++TotalMarks;
                }
                else
                {
                    MessageBox.Show("Incorrect answer. FALSE is the correct answer.Let me explain:");
                    QuizzesAnswers();
                }
                Question16.Visibility = Visibility.Collapsed;
                Question17.Visibility = Visibility.Visible;
            }
            else if (counter == 17)
            {
                if (Question17B.IsChecked == true)
                {
                    MessageBox.Show("You're a cybersecurity pro!");
                    QuizzesAnswers();
                    TotalMarks = ++TotalMarks;
                }
                else
                {
                    MessageBox.Show("Incorrect answer. B is the correct answer.Let me explain:");
                    QuizzesAnswers();
                }
                Question17.Visibility = Visibility.Collapsed;
                QuizsDisplay.Visibility = Visibility.Collapsed;
                MessageBox.Show("Quiz completed! Your total score is " + TotalMarks + " out of 17.");
                displaying.Visibility = Visibility.Visible;
            }
            counter = counter + 1;
        }

        public void QuizzesAnswers()
        {

            foreach (string keyword in Answers.Keys)
            {
                if ("Question1B".Contains(keyword))
                {

                    MessageBox.Show(Answers[keyword]);
                    break;
                }

            }

        }

        /// <summary>
        /// NLP-powered response generator that understands multiple phrasing variations
        /// </summary>
        private string GenerateNLPResponse(string userInput)
        {
            userInput = userInput.ToLower().Trim();

            // ============================================================
            // STEP 1: Detect INTENT using Regex patterns (NLP!)
            // ============================================================
            string intent = DetectIntentNLP(userInput);

            // ============================================================
            // STEP 2: Extract ENTITY (what they're asking about)
            // ============================================================
            string entity = ExtractEntityNLP(userInput, intent);

            // ============================================================
            // STEP 3: Generate response based on intent + entity
            // ============================================================
            switch (intent)
            {
                case "set_name":
                    return HandleSetName(userInput);

                case "greeting":
                    return HandleGreeting(userInput);

                case "ask_definition":
                    return HandleDefinition(entity, userInput);

                case "ask_tips":
                    return HandleTips(entity, userInput);

                case "ask_explain":
                    return HandleExplain(entity, userInput);

                case "ask_more_info":
                    return HandleMoreInfo(entity, userInput);

                case "summary":
                    return Summary();

                case "goodbye":
                    MessageBox.Show($"Goodbye {name}! Stay safe online! 👋");
                    Environment.Exit(0);
                    return "";

                case "feeling":
                    return HandleFeeling(userInput);

                case "question":
                    return HandleQuestion(userInput);

                default:
                    // Try your existing dictionaries first
                    string dictResponse = RespondSystem(userInput, name);
                    if (!dictResponse.Contains("Sorry I don't understand that."))
                    {
                        return dictResponse;
                    }

                    // Try tips dictionary
                    string tipsResponse = TipsMethod(userInput);
                    if (!tipsResponse.Contains("Sorry I don't understand that."))
                    {
                        return tipsResponse;
                    }

                    // Try definition dictionary
                    string defResponse = Defination(userInput, name);
                    if (!defResponse.Contains("Sorry I don't understand that."))
                    {
                        return defResponse;
                    }

                    // If all else fails, ask a clarifying question (NLP-friendly!)
                    return GenerateClarifyingQuestion(userInput);
            }
        }

        // ============================================================
        // SUPPORTING NLP METHODS
        // ============================================================

        /// <summary>
        /// Detects the user's intent using regex pattern matching
        /// </summary>
        private string DetectIntentNLP(string input)
        {
            // Priority order - check most specific patterns first

            // 1. Name detection
            if (Regex.IsMatch(input, @"(my name is|i am|call me|i'm|name's)\s+\w+", RegexOptions.IgnoreCase))
                return "set_name";

            // 2. Greetings
            if (Regex.IsMatch(input, @"\b(hi|hello|hey|howdy|greetings|sup|yo|good morning|good afternoon|good evening)\b", RegexOptions.IgnoreCase))
                return "greeting";

            // 3. Goodbye
            if (Regex.IsMatch(input, @"\b(bye|goodbye|see you|later|cya|farewell|take care|have a good day)\b", RegexOptions.IgnoreCase))
                return "goodbye";

            // 4. Summary/reminder
            if (Regex.IsMatch(input, @"\b(summary|summarize|preview|review|overview|previous conversation|remind me)\b", RegexOptions.IgnoreCase))
                return "summary";

            // 5. More info / explain in detail
            if (Regex.IsMatch(input, @"(tell me more|give me more|elaborate|explain in more detail|go deeper)", RegexOptions.IgnoreCase))
                return "ask_more_info";

            // 6. Explain - but not definition (covers "explain X to me")
            if (Regex.IsMatch(input, @"(explain|tell me about|give me info about)\s+\w+", RegexOptions.IgnoreCase))
                return "ask_explain";

            // 7. Definition - "what is X", "define X"
            if (Regex.IsMatch(input, @"(what (is|are)|define|meaning of|what does.*mean)\s+\w+", RegexOptions.IgnoreCase))
                return "ask_definition";

            // 8. Tips - "tips for X", "how to protect X"
            if (Regex.IsMatch(input, @"(tip|tips|advice|suggestion|best practices|how (can|do) i|how to)\s+(\w+)", RegexOptions.IgnoreCase))
                return "ask_tips";

            // 9. Feelings - "I'm worried", "I'm curious"
            if (Regex.IsMatch(input, @"\b(worried|curious|frustrated|confused|scared|anxious)\b", RegexOptions.IgnoreCase))
                return "feeling";

            // 10. Any question mark - treat as a general question
            if (input.Contains("?"))
                return "question";

            // 11. Check if it's in your existing dictionaries
            foreach (string keyword in memory.Keys)
            {
                if (input.Contains(keyword))
                    return "dictionary_match";
            }

            // 12. Check if it's in tips dictionary
            foreach (string keyword in TipsMemory.Keys)
            {
                if (input.Contains(keyword))
                    return "tips_match";
            }

            // 13. Check if it's in definitions dictionary
            foreach (string keyword in defination.Keys)
            {
                if (input.Contains(keyword))
                    return "definition_match";
            }

            return "unknown";
        }

        /// <summary>
        /// Extracts the main entity/topic from the user's input
        /// </summary>
        private string ExtractEntityNLP(string input, string intent)
        {
            input = input.ToLower();
            Match match;

            switch (intent)
            {
                case "set_name":
                    match = Regex.Match(input, @"(?:my name is|i am|call me|i'm|name's)\s+(\w+)", RegexOptions.IgnoreCase);
                    if (match.Success) return match.Groups[1].Value;
                    break;

                case "ask_definition":
                    // Try multiple patterns to extract the term
                    match = Regex.Match(input, @"what (is|are)\s+(\w+)", RegexOptions.IgnoreCase);
                    if (match.Success) return match.Groups[2].Value;

                    match = Regex.Match(input, @"define\s+(\w+)", RegexOptions.IgnoreCase);
                    if (match.Success) return match.Groups[1].Value;

                    match = Regex.Match(input, @"meaning of\s+(\w+)", RegexOptions.IgnoreCase);
                    if (match.Success) return match.Groups[1].Value;

                    match = Regex.Match(input, @"what does\s+(\w+)\s+mean", RegexOptions.IgnoreCase);
                    if (match.Success) return match.Groups[1].Value;
                    break;

                case "ask_tips":
                    match = Regex.Match(input, @"tips?\s+(for|about|on)?\s*(\w+)", RegexOptions.IgnoreCase);
                    if (match.Success) return match.Groups[2].Value;

                    match = Regex.Match(input, @"how (can|do) i (protect|secure|avoid|prevent|stop)\s+(\w+)", RegexOptions.IgnoreCase);
                    if (match.Success) return match.Groups[3].Value;

                    match = Regex.Match(input, @"how to (protect|secure|avoid|prevent|stop)\s+(\w+)", RegexOptions.IgnoreCase);
                    if (match.Success) return match.Groups[2].Value;

                    match = Regex.Match(input, @"best practices for\s+(\w+)", RegexOptions.IgnoreCase);
                    if (match.Success) return match.Groups[1].Value;
                    break;

                case "ask_explain":
                case "ask_more_info":
                    match = Regex.Match(input, @"(tell me about|explain|give me info about|elaborate on)\s+(\w+)", RegexOptions.IgnoreCase);
                    if (match.Success) return match.Groups[2].Value;
                    break;

                case "feeling":
                    match = Regex.Match(input, @"\b(worried|curious|frustrated|confused|scared|anxious)\b", RegexOptions.IgnoreCase);
                    if (match.Success) return match.Groups[1].Value;
                    break;

                case "question":
                    // Try to extract the main subject from a question
                    match = Regex.Match(input, @"\b(what|why|how|when|where|who)\s+(is|are|do|does|can)\s+(\w+)", RegexOptions.IgnoreCase);
                    if (match.Success) return match.Groups[3].Value;
                    break;
            }

            // Try to find any known keyword in the input
            foreach (string keyword in memory.Keys)
            {
                if (input.Contains(keyword))
                    return keyword;
            }

            return input; // fallback - return the whole input
        }

        // ============================================================
        // HANDLER METHODS FOR EACH INTENT
        // ============================================================

        private string HandleSetName(string input)
        {
            var match = Regex.Match(input, @"(?:my name is|i am|call me|i'm|name's)\s+(\w+)", RegexOptions.IgnoreCase);
            if (match.Success)
            {
                name = match.Groups[1].Value.ToUpper();
                string[] responses = {
            $"Nice to meet you, {name}!  I'm your cybersecurity awareness assistant. How can I help you stay safe online?",
            $"Hello, {name}!  I'm here to help you learn about cybersecurity. What would you like to know?",
            $"Great to meet you, {name}!  I can teach you about phishing, malware, passwords, and more. Where should we start?",
            $"Welcome, {name}! " +
            $" I'm your personal cybersecurity guide. Ask me anything about staying safe online!"
        };
                return responses[new Random().Next(responses.Length)];
            }
            return "I didn't quite catch your name. Could you tell me again? ";
        }

        private string HandleGreeting(string input)
        {
            string[] responses = {
        $"Hello there! I'm your cybersecurity awareness bot. What's on your mind today, {name}?",
        $"Hi!  Ready to learn about cybersecurity? Ask me about phishing, malware, passwords, or anything else!",
        $"Hey there!  I'm here to help you stay safe online. What would you like to learn about today?",
        $"Greetings! I'm your digital security buddy. How can I help you protect yourself online?"
    };
            return responses[new Random().Next(responses.Length)];
        }

        private string HandleDefinition(string entity, string input)
        {
            if (string.IsNullOrEmpty(entity)) entity = input;

            // Call your existing Defination method
            string definition = Defination(entity, name);

            // If your Defination method returns the default "Sorry" message, try searching for it
            if (definition.Contains("Sorry I don't understand that."))
            {
                // Try to find a matching keyword in your dictionary
                foreach (string keyword in memory.Keys)
                {
                    if (entity.Contains(keyword) || keyword.Contains(entity))
                    {
                        return memory[keyword];
                    }
                }
                return $"I don't have a definition for '{entity}' yet.  Here's what I can help with:\n• Cybersecurity topics\n• Password tips\n• Threat definitions\n\nWhat would you like to learn about? 🤔";
            }

            return $" **{entity.ToUpper()}**: {definition}\n\nWould you like me to give you tips on {entity} as well?";
        }

        private string HandleTips(string entity, string input)
        {
            if (string.IsNullOrEmpty(entity)) entity = input;

            // Call your existing TipsMethod
            string tips = TipsMethod(input);

            // If TipsMethod returns the default "Sorry" message, try searching
            if (tips.Contains("Sorry I don't understand that."))
            {
                foreach (string keyword in TipsMemory.Keys)
                {
                    if (entity.Contains(keyword) || keyword.Contains(entity))
                    {
                        return $" **Cybersecurity Tips**:\n{TipsMemory[keyword]}\n\nAnything else you'd like to know?";
                    }
                }
                return $"I don't have specific tips for '{entity}' yet.  Try asking about:\n• Password tips\n• Phishing prevention\n• Malware protection\n• General cybersecurity\n\nWhat would you like to know more about?";
            }

            return $" **Tips for {entity.ToUpper()}**:\n{tips}\n\nIs there anything else you'd like to learn about?";
        }

        private string HandleExplain(string entity, string input)
        {
            if (string.IsNullOrEmpty(entity)) entity = input;

            // Call your existing Explaination method
            string explanation = Explaination(entity);

            if (explanation.Contains("Sorry I don't understand that."))
            {
                // Try to find in your dictionaries
                foreach (string keyword in memory.Keys)
                {
                    if (entity.Contains(keyword) || keyword.Contains(entity))
                    {
                        return $" **{keyword.ToUpper()}**:\n{memory[keyword]}\n\nWould you like to know more?";
                    }
                }
                return $"I don't have detailed information about '{entity}' yet.  What other cybersecurity topics interest you?";
            }

            return $" **{entity.ToUpper()}**:\n{explanation}\n\nAnything else you'd like me to explain?";
        }

        private string HandleMoreInfo(string entity, string input)
        {
            if (string.IsNullOrEmpty(entity)) entity = input;

            // Try to find detailed info
            string detail = Explaination(entity);
            if (!detail.Contains("Sorry I don't understand that."))
            {
                return $" **More about {entity.ToUpper()}**:\n{detail}\n\nIs there anything specific you'd like to know?";
            }

            // Try the definition
            string definition = Defination(entity, name);
            if (!definition.Contains("Sorry I don't understand that."))
            {
                return $" **More about {entity.ToUpper()}**:\n{definition}\n\nWould you like tips on this topic too?";
            }

            return $"I'd love to tell you more about '{entity}'!  Could you tell me what you already know, or ask a specific question?";
        }

        private string HandleFeeling(string input)
        {
            string feeling = "";
            if (input.Contains("worried")) feeling = "worried";
            else if (input.Contains("curious")) feeling = "curious";
            else if (input.Contains("frustrated")) feeling = "frustrated";
            else if (input.Contains("confused")) feeling = "confused";
            else if (input.Contains("scared")) feeling = "scared";
            else if (input.Contains("anxious")) feeling = "anxious";

            Dictionary<string, string[]> feelingResponses = new Dictionary<string, string[]>
    {
        { "worried", new string[] {
            $"I understand you're worried, {name}.  Cybersecurity can feel overwhelming, but I'm here to help you learn step by step. What specifically concerns you?",
            $"No need to worry, {name}!  Being informed is the first step to staying safe. What would you like to learn about?",
            $"I hear you, {name}.  Let's tackle your cybersecurity concerns together. What aspect worries you the most?"
        }},
        { "curious", new string[] {
            $"That's great, {name}!  Curiosity is the best way to learn about cybersecurity. What topic fascinates you?",
            $"I love your curiosity, {name}!  Let's dive into cybersecurity together. Where would you like to start?",
            $"Your curiosity is your superpower, {name}!  What cybersecurity topic would you like to explore?"
        }},
        { "frustrated", new string[] {
            $"I understand it can be frustrating, {name}.  Let me simplify cybersecurity for you. What's bothering you?",
            $"Take a deep breath, {name}!  I'll break this down so it's easy to understand. What's confusing you?",
            $"Don't worry, {name}!  I'm here to make cybersecurity simple. What would you like me to clarify?"
        }},
        { "confused", new string[] {
            $"Let me help clear things up, {name}!  Cybersecurity terms can be confusing. What would you like me to explain?",
            $"No need to be confused, {name}!  I'll explain it in simple terms. What would you like to understand better?",
            $"I'll make this crystal clear for you, {name}!  What cybersecurity concept is confusing you?"
        }},
        { "scared", new string[] {
            $"It's okay to be scared, {name}!  Cybersecurity threats can seem scary, but knowledge is power. What's worrying you?",
            $"You're safe here, {name}!  Let me help you understand how to protect yourself. What's on your mind?",
            $"Don't be scared, {name}!  I'm here to teach you how to stay safe online. What would you like to learn first?"
        }},
        { "anxious", new string[] {
            $"I hear your anxiety, {name}.  Cybersecurity doesn't have to be stressful. Let's take it step by step. What's troubling you?",
            $"Breathe, {name}!  I'll guide you through cybersecurity at your own pace. Where would you like to start?",
            $"You've got this, {name}!  I'll make cybersecurity easy and stress-free. What would you like to know?"
        }}
    };

            if (feelingResponses.ContainsKey(feeling))
            {
                string[] responses = feelingResponses[feeling];
                return responses[new Random().Next(responses.Length)];
            }

            return $"I hear you, {name}.  How about we learn something about cybersecurity together? What would you like to know?";
        }

        private string HandleQuestion(string input)
        {
            // Try to find a match in your dictionaries
            string response = RespondSystem(input, name);
            if (!response.Contains("Sorry I don't understand that."))
            {
                return response;
            }

            // Try to extract the main topic
            var match = Regex.Match(input, @"\b(what|why|how|when|where|who)\s+(is|are|do|does|can)\s+(\w+)", RegexOptions.IgnoreCase);
            if (match.Success)
            {
                string topic = match.Groups[3].Value;
                foreach (string keyword in memory.Keys)
                {
                    if (keyword.Contains(topic) || topic.Contains(keyword))
                    {
                        return $"That's a great question!  Let me check...\n\n{memory[keyword]}\n\nDoes that help?";
                    }
                }
            }

            return GetHelpfulSuggestion(input);
        }

        private string GenerateClarifyingQuestion(string input)
        {
            string[] clarifyingQuestions = {
        $"I'm not quite sure I understand, {name}.  Could you rephrase that or ask about a specific cybersecurity topic?",
        $"Hmm, that's a new one for me!  Can you tell me more about what you're looking for?",
        $"I want to help you, {name}!  Could you ask your question in a different way?",
        $"Let me make sure I understand correctly, {name}.  Are you asking about cybersecurity, passwords, phishing, or something else?",
        $"I didn't quite catch that, {name}!  Here are some topics I know about:\n• Cybersecurity basics\n• Password protection\n• Phishing scams\n• Malware and viruses\n• Online privacy\n\nWhat would you like to learn?",
        $"You've stumped me, {name}!  But I'm always learning. Could you try asking about:\n• Definitions (e.g., 'what is ransomware?')\n• Tips (e.g., 'tips for secure passwords')\n• Explanations (e.g., 'explain phishing')\n\nHow can I help you?"
    };
            return clarifyingQuestions[new Random().Next(clarifyingQuestions.Length)];
        }

        private string GetHelpfulSuggestion(string input)
        {
            string[] suggestions = {
        $"I'm always here to help, {name}!  Here are some things you can ask about:\n• Cybersecurity definitions\n• Password protection tips\n• Phishing prevention\n• Malware and virus information\n• Online privacy advice\n\nWhat interests you?",
        $"You can ask me anything about cybersecurity, {name}!  Try asking:\n• 'What is phishing?'\n• 'Tips for strong passwords'\n• 'How to protect my data'\n\nWhat would you like to learn?",
        $"I'm your cybersecurity buddy, {name}!  Ask me about:\n• Common cyber threats\n• Security best practices\n• How to stay safe online\n\nShoot me a question! 😊"
    };
            return suggestions[new Random().Next(suggestions.Length)];
        }



    }
}