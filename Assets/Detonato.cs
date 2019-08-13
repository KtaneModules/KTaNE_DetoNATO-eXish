using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using KModkit;

public class Detonato : MonoBehaviour
{
    public KMSelectable[] buttons;
    public TextMesh screenText;
    public MeshRenderer[] stageLights;
    public KMBombInfo info;
    
    public Material off;
    public Material on;
    
    string[] buttonLabels = new string[4];
    string screenLabel;
    
    string[] words = new[] {
        "Alpha", "Alfa", "Aleph", "Alef", "Alepha", "Alefa",
        "Beta", "Bayta", "Beeta", "Baeta", "Baita", "Beata",
        "Charlie", "Charly", "Charlee", "Sharlie", "Sharly", "Sharlee",
        "Delta", "Daleta", "Dellta", "Dwellta", "Dealta", "Deelta",
        "Echo", "Ecko", "Eco", "Ecco", "Gecko", "Eggo",
        "Foxtrot", "Fockstrot", "Flockstrot", "Foxstrut", "Foxstep", "Flockstep",
        "Golf", "Gulf", "Golph", "Gulph", "Goff", "Guff",
        "Hotel", "Hotell", "Hotale", "Hoetell", "Wholetale", "Holetel",
        "India", "Inndia", "Indya", "Inndya", "Indeer", "Indear",
        "Juliett", "Juliette", "Julette", "Jouleliett", "Jouleliette", "Joulelette",
        "Kilo", "Keylo", "Kilow", "Keylow", "Kielo", "Kylo",
        "Lima", "Leema", "Lema", "Lemma", "Leemma", "Limma",
        "Mike", "Mic", "Myke", "Mice", "Mythe", "Mite",
        "November", "Nomember", "Nobemver", "Nofember", "Nobemfer", "Nobemmer",
        "Oscar", "Oskar", "Ozcar", "Ozkar", "Ozscar", "Ozzy Osbourne",
        "Papa", "Parpa", "Papar", "Parpar", "Pappa", "Pappar",
        "Quebec", "Cuebec", "Queuebec", "Quibec", "Quirkbec", "Kwerebec",
        "Romeo", "Romio", "Roomeo", "Rodeo", "Rolemeo", "Rollmeo",
        "Seirra", "Sierra", "Seeara", "Seaara", "Tierra", "T-ARA",
        "Tango", "Tankgo", "Tongo", "Tanga", "Taiga", "Tangle",
        "Uniform", "Unifrom", "Unifoam", "Youniform", "Unitform", "Uniqueform",
        "Victor", "Vector", "Wicktor", "Vicster", "Vicktor", "Vaccines",
        "Whisky", "Risky", "Withsky", "Wisky", "Discy", "Frisky",
        "X-ray", "Ex-ray", "Xenoblade", "X01", "Hexrray", "XSS",
        "Yankee", "Yanky", "Yankie", "Yanking", "Janky", "Junky",
        "Zulu", "Zebra", "Zenoblade", "Zoolu", "Zoo", "Zoom"
    };
    string[] keys = new[] {
        "ABCDEFGHIJKLMNOPQRSTUVWXYZ", "ZGYUAQNBRVHFOTIXEDKSWCMLPJ", "ZEROYUDKFMAHINGXPQWVCJBSTL", "PCNJXDOTSMULABYWFRKVZEGQHI", "OKLGYCPVTHNWXBZAMURDJFIQES", "JEYBWFAMSDZIPHRTVONQXKULGC",
        "BACDEFGHIJKLMNOPQRSTUVWXYZ", "LKEJVSQFYWPHMTUZIROXBGADNC", "CYXTPFRKZEQULJBWGVIHDSNOAM", "IGFTVKRLHCPJNBSDWAZMXOQUYE", "VDTIGEFAJQLRUHMBOPYKWXZCSN", "EYSMXBNDFPQGRJVHLCITKOUWAZ",
        "CABDEFGHIJKLMNOPQRSTUVWXYZ", "TPYVCKERJIGHSNOWAZMXUFBDQL", "FWEDKIYARTQUGSHJPONBCZLMVX", "QTNEIHPYZLOKBGWRAUXFDJSVMC", "EIFWGZBUYKQVNXHPLSDAOJRMTC", "QPEKJTZSYDACXRVHOLFIGMWBUN",
        "DABCFGHIJKLMOPQSTVWXYZRUNE", "RIVEXPQJTMOFGZYCDUAHLKNBWS", "COMPUTERJADYGIQHWVZKNFBXLS", "CAVERDNWTBULPFIYGHJXOSMKZQ", "CARDSBPOVKHGEZJQYLUNWTFIXM", "KVGPIUOJALFZBXNDWHTSCYMQRE",
        "EABCDFGHIJKLMNOPQRSTUVWXZY", "QKJABUZDPYTWXERSCOFNMVGHLI", "VEZYSRTBUMQDAFILGCKHXWJOPN", "AQMXNLIVCGKHTBWEROFUZSJPDY", "RMGKAJCLOPWDEZXTNIYQUFBSHV", "HLCWQUOTZEAKFRVMYBPNIDXSJG",
        "FABCDEGHIKJLMNPOQRSTUVWXYZ", "TRWSALEFODGPQHNBKVJIYXMCUZ", "PMBWKCGXRHLJNVIEAQUSDFYOZT", "YGDCZPBQWMSENXLRATHVKFUIJO", "DMIPGVUZJAYWFERHBLSNKCOXTQ", "BIRDSVQEAHMLGZNOFJKWTXUYPC",
        "FABCDGJKMPQRSTUVWZYXHOLEIN", "DHZVRPWUAOCGFXQNEKYLSTBIMJ", "MWGSJCEXPRTLQBIKFDVUHNZAYO", "JTIQMOLHZKPRWSACEYXFNGBDVU", "PIMJOQADNLKCGVWZTSRFXHBYUE", "HTEKWOFUZMAQSGJVLIBXDPYNCR",
        "HBCDEFGJKLNPQSTUVWXYZMARIO", "MHALOGSNWQTZXDPURBEKYJFIVC", "GIWROCBKNTDZVEXMPFJHLUSQAY", "GAYTSIQEDXKBZJLWFRNHPOCMVU", "BCSPUTLJAYDMIFQOXHNZRVKEWG", "TWJPADMOCFKSLZQXYREUVGNBHI",
        "IABCDEFGHJKLMNOPQRSTUVXWYZ", "SNOWGLZDIUFETAKCMXQRBHVJYP", "RNXSTOAFLHJUDIEWMZVPBCYGKQ", "CXPEOUFJKAZDSHVBQIRLMGWNYT", "RUDOLPHGVCKFXNYISMEBQZTAWJ", "JOHNECLYDPKMUTAXGZSRBQIVWF",
        "JABCDEFGHIKLMNOPQRSTZYXWVU", "QVOXSHZIJFAGYNWTDCLEKPUMRB", "UOJFBLYTNHPCSKWRZIQAMEVGXD", "RQMZXOWKEIACPSGLYDTUVFJNBH", "YBKVEGHWOXMIPFQUATNDCJLSRZ", "VECMDGLUPSXKAIYTQHBNFJZWRO",
        "KZABCDEFGHIJLMNOPQRSTUVWXY", "OGQSDUJPCBFITMVEXZWHALNRYK", "MVAHCJITQLSDKYZNXERWFBPGUO", "SWVXMKTCNZYAUPGFIQOEDLBHRJ", "RMNFZHXKGASWIQBPUVJCEODTYL", "BRZMTIXNJGHYSDLAWVPKQUEOCF",
        "LABCDEFGHIJZKMNOPQRSTUVWYX", "YJBUOZNHTDKQGMIEVCAPFSLXRW", "OBWDYNMEIVSPAKFJRLZHUTCXGQ", "FYKRVLPCIADXMNWBEQZHTGUJOS", "XMVCWUIKEROBJADTZFGPSNQHYL", "SEICHKNUJPDMFROYAVTZWGQBXL",
        "MABCDEFGHIJKLNOPQRSTUVWXYZ", "DRXPWQFUMZCLGHESOVTJNYBKAI", "ADBUTXYKGPRCZEJVWOMINSQFHL", "HKPLZXCSYQAIVMJBREUDWTFGNO", "NYWCTRUGKBLZFIVSAJHQMPXDOE", "RFMWKPYZJOCTXDABELGHIUVSNQ",
        "NABCDEFGHIJKLMOPQRSTUVWYZX", "NABCDEFGHIKJLMOPQRSTUVWYZX", "NABCDEFGHIJKLMOQPRSTUVWYZX", "NABCDFEGHIJKLMOPQRSTUVWYZX", "NABCDEFGHIJKLMOPQRTSUVWYZX", "NABCDEFHGIJKLMOPQRSTUVWYZX",
        "OABCDEFGHIJKLMNPQRSTUVWXYZ", "XLEQKGDUTYMSVWNACHFIOBPZRJ", "EGRNFBUXCAOQHSYWKZLPMJVIDT", "FAUEONWGRTSBMLICVZJKDQPHXY", "JRAEZTONMWYDFQBGPXVSLUCHKI", "KABULDNGEHIYRVTQOCJZWMXPFS",
        "PABCDEFGHIJKLMNOQRSTUVWXYZ", "QDVOTJPSRGKIUBCYALEXWHMFNZ", "ADWIPFZVEUTBQRGMJLNYXKOSHC", "EJPKCVHIGQDULWAXZYTSMBRFNO", "YZCLURENJWIMODSXHTAQGKVBFP", "XMIQGSEVFOUCDNLBYJWAZPTRHK",
        "QABCDEFGHIJKLMNOPRSTUVWXYZ", "WMSCHPZEATVKOLBYNUIFDRGJXQ", "YVIZSWLAQXJNMGTURBOFCHEDPK", "LOERBGTAHDXIZPQKVCMJNSFUWY", "FKXASWZVQBTEYMDCIRNGOUPLJH", "OGIYLNRPXTSWEFAUHDCMVZKBQJ",
        "RABCDEFGHIJKLMNOPQSTUVWXYZ", "QLIUMTOKBRFZESDHJNGVPYAXWC", "UXIBFKJMDCOLSGVYQETPHRZNAW", "ALSZHOCNQFPXWKUMBTEIGRJYDV", "BIWTDERLHXFVMGPUNZYSJKQOCA", "YHUZMPABWTGREIDKLNSOVXCQFJ",
        "SABCDEFGHIJKLMNOPQRTUVWXYZ", "AZVNWLCYKRGTMEPDSHUQJXOBFI", "FCHJDUNPVZXSQOIBRAMTKWYLGE", "KZECIQGVRANOBWSJLFDYTMPXUH", "STABCDEFGHIJKLMNOPQRUVWXYZ", "QRIMDUZHCKJBTLYVNGAWXOESFP",
        "TEKABCDFGHIJLMNOPQRSUVWXYZ", "OGBPAWTDJQIHVNYZSEXMCLUFKR", "BJKTDEWVPOZRIAGYUQSCLFMNHX", "PGXSCKTOBVRQIYNEWMJALHDUZF", "YVXMGARCOJHPNLQKUEZDFITBWS", "EVPTSZKCQRWMHULDAYOBJFNXGI",
        "UABCDEFGHIJKLMNOPQRSTVWXYZ", "FQTBLXIJCSOUMEKZVPANGWHYRD", "ZNFQGOTIAHYCSPXLWRDEKVJBUM", "EVAZCJODNFBQUKXYTLGHSIPRWM", "OVRQBPTHSWJMGLXDYUNFZAKIEC", "LWCUQHNODSMVEXAITYJZFBRGPK",
        "VACBNDEFGHIJKLMOPQRSTUWZXY", "LINEWQFCKAPGBTZDRVOXYJMUHS", "UGYJAIBWMVLXNOTFQPEKSZHRDC", "DNFUCOYXPVSRKGTQJLHMBEWIAZ", "BHNLKRUTCJXWOIDZSEGMYQVAPF", "VAXDHKIQGMCEPJYOLSNWBFURTZ",
        "WABCDEFGHIJKLMNOPQRXYZSTUV", "RABCDEFGHIJKLMNOPQWXYZSTUV", "YZBEIDXFWQGMNOJLUHPVKRCTSA", "CYFJQKALIBMWNDOGXPHVRUSTEZ", "JFLMZHBCWURGAIDPYKXOEVSQNT", "XLRJSPUFMOTZQWKEYDBIHNAGVC",
        "XABCDEFGHIJKLMNOZQRSTUYWVP", "FUAMPNCDIVOLBWXYHZKQGRETSJ", "SHULKONQWXREPGYZBAVITMFJCD", "DARTJPGIYMESCOBNHKXWLUQZFV", "MAZEQDIVOTJWXFKSUCNPRGLYBH", "HAXRBGOQSVDUJFPTYEKLNICWMZ",
        "YABCDEFGHIJKLMNOPQRSTUVXWZ", "XUVOBMGFIRCAQHSJYTKEZLNWDP", "ZDLEAKCSXJBUOPTHRGMFWVIQYN", "EVFSMRZTOUJPXDGKQINYHCLBWA", "TDOXGERHNIUWFJMLCPSYKBVQZA", "MOVKALGIWTPRJZEUSNCHDFXBYQ",
        "ZAPBCDEFGHIJKLMNOYQRSTUXWV", "XNHYBZQIOCUFKDJPSMRLWAGTEV", "CHALKSOBPGTZFYIDUVQRWXJENM", "TVKQNOHBEMFZCXSYLIGWPRUJAD", "PGJBDKRWQHSEUXIYNCTZMOALVF", "LDQXVMYOZWSJGHTCFEKRUIABPN"
    };
    
    int stage = 0;
    int textId = 0;
    bool isActive = false;
    
    static int _moduleIdCounter = 1;
    int _moduleId = 0;

    void Start()
    {
        _moduleId = _moduleIdCounter++;
        GetComponent<KMBombModule>().OnActivate += ActivateModule;
    }

    void Init()
    {
        for(int i = 0; i < buttons.Length; i++)
        {
            int j = i;
            buttons[i].OnInteract += delegate () { OnPress(j); return false; };
        }
    }
    
    IEnumerator Randomize(bool wait = false, bool wordOnly = false)
    {
        if (!wordOnly && wait)
        {
            for(int i = 0; i < buttons.Length; i++)
            {
                TextMesh buttonText = buttons[i].GetComponentInChildren<TextMesh>();
                buttonText.text = "";
                yield return new WaitForSeconds(0.15f);
            }
            yield return new WaitForSeconds(0.4f);
        }
        
        if (wait)
        {
            screenText.text = "";
            yield return new WaitForSeconds(1.0f);
        }
        
        if (!wordOnly)
        {
        
            buttonLabels[0] = "" + (char)UnityEngine.Random.Range('A', 'Z' + 1);
            buttonLabels[1] = "" + (char)UnityEngine.Random.Range('A', 'Z' + 1);
            do
            {
                buttonLabels[1] = "" + (char)UnityEngine.Random.Range('A', 'Z' + 1);
            } while (buttonLabels[1] == buttonLabels[0]);
            buttonLabels[2] = "" + (char)UnityEngine.Random.Range('A', 'Z' + 1);
            do
            {
                buttonLabels[2] = "" + (char)UnityEngine.Random.Range('A', 'Z' + 1);
            } while (buttonLabels[2] == buttonLabels[0] || buttonLabels[2] == buttonLabels[1]);
            buttonLabels[3] = "" + (char)UnityEngine.Random.Range('A', 'Z' + 1);
            do
            {
                buttonLabels[3] = "" + (char)UnityEngine.Random.Range('A', 'Z' + 1);
            } while (buttonLabels[3] == buttonLabels[0] || buttonLabels[3] == buttonLabels[1] || buttonLabels[3] == buttonLabels[2]);

            for(int i = 0; i < buttons.Length; i++)
            {
                TextMesh buttonText = buttons[i].GetComponentInChildren<TextMesh>();
                buttonText.text = buttonLabels[i];
                yield return new WaitForSeconds(0.15f);
            }
        }
        
        yield return new WaitForSeconds(0.4f);
        textId = UnityEngine.Random.Range(0, 156);
        screenText.text = words[textId];
        
        Debug.LogFormat("[DetoNATO #{0}] Stage {1}. The word is \"{2}\". The letters are: {3}.", _moduleId, stage + 1, screenText.text, buttonLabels[0] + ", " + buttonLabels[1] + ", " + buttonLabels[2] + " and " + buttonLabels[3]);
        Debug.LogFormat("[DetoNATO #{0}] The key is \"{1}\".", _moduleId, keys[textId]);
        isActive = true;
        
    }

    void ActivateModule()
    {
        StartCoroutine(Randomize());
        Init();
    }
    
    IEnumerator ReactivateModule()
    {
        isActive = false;
        yield return null;
        StartCoroutine(Randomize(true, true));
    }
    
    IEnumerator StageLight(int st)
    {
        yield return new WaitForSeconds(0.15f);
        stageLights[stage++].material = on;
    }
    
    void OnPress(int pressedButton)
    {
        GetComponent<KMAudio>().PlayGameSoundAtTransform(KMSoundOverride.SoundEffect.ButtonPress, transform);
        GetComponent<KMSelectable>().AddInteractionPunch();
        if (!isActive)
        {
            return;
        }
        bool correct = true;
        int pos = 0;
        char s1 = '0', s2 = '0';
        switch (stage)
        {
            case 0:
                s1 = keys[textId][25];
                for (int i = 0; i < buttons.Length; i++)
                {
                    if (keys[textId].IndexOf(buttonLabels[i]) < keys[textId].IndexOf(buttonLabels[pressedButton]))
                    {
                        correct = false;
                    }
                    if (keys[textId].IndexOf(buttonLabels[i]) < keys[textId].IndexOf(s1))
                    {
                        s1 = buttonLabels[i][0];
                    }
                }
                break;
            case 1:
                s1 = keys[textId][0];
                for (int i = 0; i < buttons.Length; i++)
                {
                    if (keys[textId].IndexOf(buttonLabels[i]) > keys[textId].IndexOf(buttonLabels[pressedButton]))
                    {
                        correct = false;
                    }
                    if (keys[textId].IndexOf(buttonLabels[i]) > keys[textId].IndexOf(s1))
                    {
                        s1 = buttonLabels[i][0];
                    }
                }
                break;
            case 2:
                s1 = keys[textId][25];
                s2 = keys[textId][25];
                for (int i = 0; i < buttons.Length; i++)
                {
                    if (keys[textId].IndexOf(buttonLabels[i]) < keys[textId].IndexOf(buttonLabels[pressedButton]))
                    {
                        pos++;
                    }
                    if (keys[textId].IndexOf(buttonLabels[i]) < keys[textId].IndexOf(s2))
                    {
                        s2 = buttonLabels[i][0];
                    }
                }
                correct = (pos == 1);
                for (int i = 0; i < buttons.Length; i++)
                {
                    if (keys[textId].IndexOf(buttonLabels[i]) > keys[textId].IndexOf(s2) && keys[textId].IndexOf(buttonLabels[i]) < keys[textId].IndexOf(s1))
                    {
                        s1 = buttonLabels[i][0];
                    }
                }
                s2 = '0';
                break;
            case 3:
                s1 = keys[textId][0];
                s2 = keys[textId][0];
                for (int i = 0; i < buttons.Length; i++)
                {
                    if (keys[textId].IndexOf(buttonLabels[i]) > keys[textId].IndexOf(buttonLabels[pressedButton]))
                    {
                        pos++;
                    }
                    if (keys[textId].IndexOf(buttonLabels[i]) > keys[textId].IndexOf(s2))
                    {
                        s2 = buttonLabels[i][0];
                    }
                }
                correct = (pos == 1);
                for (int i = 0; i < buttons.Length; i++)
                {
                    if (keys[textId].IndexOf(buttonLabels[i]) < keys[textId].IndexOf(s2) && keys[textId].IndexOf(buttonLabels[i]) > keys[textId].IndexOf(s1))
                    {
                        s1 = buttonLabels[i][0];
                    }
                }
                s2 = '0';
                break;
            case 4:
                s1 = buttonLabels[0][0];
                string sn = info.GetSerialNumber();
                char chr = 'A';
                for (int i = 0; i < sn.Length; i++)
                {
                    if (char.IsLetter(sn[i]))
                    {
                        chr = sn[i];
                        break;
                    }
                }
                for (int i = 0; i < buttons.Length; i++)
                {
                    if (Math.Abs(keys[textId].IndexOf(buttonLabels[i]) - keys[textId].IndexOf(chr)) < Math.Abs(keys[textId].IndexOf(buttonLabels[pressedButton]) - keys[textId].IndexOf(chr)))
                    {
                        correct = false;
                    }
                    if (Math.Abs(keys[textId].IndexOf(buttonLabels[i]) - keys[textId].IndexOf(chr)) < Math.Abs(keys[textId].IndexOf(s1) - keys[textId].IndexOf(chr)))
                    {
                        s1 = buttonLabels[i][0];
                    }
                }
                for (int i = 0; i < buttons.Length; i++)
                {
                    if (Math.Abs(keys[textId].IndexOf(buttonLabels[i]) - keys[textId].IndexOf(chr)) == Math.Abs(keys[textId].IndexOf(s1) - keys[textId].IndexOf(chr)) && keys[textId].IndexOf(buttonLabels[i]) != keys[textId].IndexOf(s1))
                    {
                        s2 = buttonLabels[i][0];
                    }
                }
                break;
            case 5:
                s1 = buttonLabels[0][0];
                for (int i = 0; i < buttons.Length; i++)
                {
                    if (Math.Abs(keys[textId].IndexOf(buttonLabels[i]) - keys[textId].IndexOf(words[textId][0])) < Math.Abs(keys[textId].IndexOf(buttonLabels[pressedButton]) - keys[textId].IndexOf(words[textId][0])))
                    {
                        correct = false;
                    }
                    if (Math.Abs(keys[textId].IndexOf(buttonLabels[i]) - keys[textId].IndexOf(words[textId][0])) < Math.Abs(keys[textId].IndexOf(s1) - keys[textId].IndexOf(words[textId][0])))
                    {
                        s1 = buttonLabels[i][0];
                    }
                }
                for (int i = 0; i < buttons.Length; i++)
                {
                    if (Math.Abs(keys[textId].IndexOf(buttonLabels[i]) - keys[textId].IndexOf(words[textId][0])) == Math.Abs(keys[textId].IndexOf(s1) - keys[textId].IndexOf(words[textId][0])) && keys[textId].IndexOf(buttonLabels[i]) != keys[textId].IndexOf(s1))
                    {
                        s2 = buttonLabels[i][0];
                    }
                }
                break;
        }
        if (correct)
        {
            StartCoroutine(StageLight(stage));
            if (stage < 5)
            {
                Debug.LogFormat("[DetoNATO #{0}] Letter {1} was correct. Advancing to stage {2}.", _moduleId, buttonLabels[pressedButton], stage + 2);
                StartCoroutine(Randomize(true));
            }
            else
            {
                screenText.text = "";
                Debug.LogFormat("[DetoNATO #{0}] Module solved!", _moduleId);
                GetComponent<KMAudio>().PlayGameSoundAtTransform(KMSoundOverride.SoundEffect.CorrectChime, transform);
                GetComponent<KMBombModule>().HandlePass();
            }
        }
        else
        {
            Debug.LogFormat("[DetoNATO #{0}] Letter {1} was incorrect (expected {2}). Resetting the stage.", _moduleId, buttonLabels[pressedButton], s1 + (s2 == '0' ? "" : " or " + s2));
            GetComponent<KMBombModule>().HandleStrike();
            StartCoroutine(ReactivateModule());
        }
    }
    
    //twitch plays
    #pragma warning disable 414
    private string TwitchHelpMessage = "Use !{0} press A to press a button with the respective label. You can also use specify the position of the button instead by using !{0} press 4";
    #pragma warning restore 414
    public KMSelectable[] ProcessTwitchCommand(string command)
    {
        command = command.Trim().ToUpperInvariant();
        if (!command.StartsWith("PRESS") || command.Length != 7) return null;

        command = command.Substring(6);
        if (command[0] >= 'A' && command[0] <= 'Z')
        {
            string label = "" + command[0];
            for (int i = 0; i < 4; i++)
            {
                if (buttonLabels[i] == label)
                {
                    return new KMSelectable[] { buttons[i] };
                }
            }
            return null;
        }
        else
        {
            int pos = (int)(command[0] - '1');
            if (pos < 0 || pos > 3) return null;
            else return new KMSelectable[] { buttons[pos] };
        }
    }
}
