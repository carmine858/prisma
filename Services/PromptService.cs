using PrismaNews.Models;

namespace PrismaNews.Services
{
    public class PromptService
    {
        private readonly Dictionary<string, string> _categoryPrompts = new()
        {
            ["politics"] = @"Analizza questo articolo politico con un approccio critico e imparziale.

1. BIAS IDEOLOGICO:
- Identifica qualsiasi bias politico o ideologico nel testo
- Evidenzia frasi che rivelano orientamenti politici impliciti o espliciti
- Valuta se l'articolo presenta in modo equilibrato le diverse posizioni

2. FRAMING PARTITICO:
- Analizza come vengono rappresentati i diversi attori politici
- Identifica cornici interpretative favorevoli o sfavorevoli a partiti/figure politiche
- Rileva l'uso di etichette o caratterizzazioni tendenziose

3. LESSICO CONFLITTUALE:
- Identifica linguaggio polarizzante o divisivo
- Evidenzia metafore belliche o espressioni che accentuano il conflitto
- Analizza la scelta di termini emotivamente carichi

4. PROSPETTIVE ALTERNATIVE:
- Suggerisci quali prospettive o informazioni mancano per un quadro più completo
- Proponi una riformulazione più equilibrata di passaggi particolarmente tendenziosi

5. CONCLUSIONE:
- Assegna un punteggio di oggettività da 0 a 100
- Sintetizza i principali elementi retorici e bias rilevati

Struttura l'analisi in sezioni chiare con esempi specifici dal testo.",

            ["environment"] = @"Analizza questo articolo ambientale con un approccio scientifico e critico.

1. TONI ALLARMISTICI:
- Identifica linguaggio catastrofico o allarmistico non supportato da dati
- Valuta se il tono emotivo è proporzionato ai fatti presentati
- Evidenzia frasi che potrebbero generare panico ingiustificato

2. FONDAMENTO SCIENTIFICO:
- Verifica se le affermazioni sono supportate da dati o ricerche citate
- Identifica generalizzazioni o semplificazioni eccessive di fenomeni complessi
- Valuta se vengono presentati margini di incertezza appropriati

3. VISIONI UNILATERALI:
- Identifica se sono rappresentate diverse prospettive sul tema
- Valuta se l'articolo considera adeguatamente opinioni contrastanti legittime
- Evidenzia eventuali falsi equilibri tra posizioni scientifiche e non-scientifiche

4. CONTESTUALIZZAZIONE:
- Valuta se i fenomeni descritti sono adeguatamente contestualizzati
- Identifica cherry-picking di dati o eventi isolati presentati come tendenze
- Verifica se sono presentate soluzioni oltre ai problemi

5. CONCLUSIONE:
- Assegna un punteggio di oggettività da 0 a 100
- Sintetizza i principali elementi retorici e bias rilevati

Struttura l'analisi in sezioni chiare con esempi specifici dal testo.",

            ["society"] = @"Analizza questo articolo su temi sociali con un approccio critico e costruttivo.

1. STEREOTIPI E GENERALIZZAZIONI:
- Identifica eventuali stereotipi su gruppi sociali, etnici o culturali
- Evidenzia generalizzazioni non supportate da dati o ricerche
- Valuta se vengono attribuite caratteristiche a intere categorie di persone

2. MORALISMO:
- Identifica giudizi morali impliciti o espliciti
- Valuta se l'articolo impone un'interpretazione morale senza presentare alternative
- Evidenzia l'uso di termini valoriali o prescrittivi

3. RETORICA POLARIZZANTE:
- Identifica se questioni complesse sono presentate in modo binario o polarizzante
- Valuta se l'articolo contribuisce al dialogo o alla divisione sociale
- Evidenzia linguaggio che potrebbe rafforzare tribalismo o divisioni

4. CONTESTUALIZZAZIONE SOCIALE:
- Valuta se i fenomeni sociali descritti sono adeguatamente contestualizzati
- Identifica se l'articolo considera i fattori strutturali rilevanti
- Evidenzia eventuali prospettive sociali mancanti o marginali

5. CONCLUSIONE:
- Assegna un punteggio di oggettività da 0 a 100
- Sintetizza i principali elementi retorici e bias rilevati

Struttura l'analisi in sezioni chiare con esempi specifici dal testo.",

            ["business"] = @"Analizza questo articolo economico/finanziario con un approccio critico e tecnico.

1. SEMPLIFICAZIONI ECONOMICHE:
- Identifica semplificazioni eccessive di fenomeni economici complessi
- Valuta se vengono considerati adeguatamente fattori multipli nelle analisi
- Evidenzia eventuali spiegazioni mono-causali di fenomeni multi-causali

2. SELEZIONE DEI DATI:
- Identifica eventuali cherry-picking di dati economici o finanziari
- Valuta se i dati sono presentati nel giusto contesto temporale e comparativo
- Evidenzia l'eventuale omissione di dati rilevanti che modificherebbero l'interpretazione

3. CORRELAZIONI VS CAUSAZIONI:
- Identifica se correlazioni sono impropriamente presentate come causazioni
- Valuta se vengono considerate variabili confondenti
- Evidenzia affermazioni causali non adeguatamente supportate

4. PROSPETTIVE ECONOMICHE:
- Valuta se sono rappresentate diverse scuole di pensiero economico
- Identifica bias verso specifiche teorie economiche
- Evidenzia se interessi particolari sono presentati come interesse generale

5. CONCLUSIONE:
- Assegna un punteggio di oggettività da 0 a 100
- Sintetizza i principali elementi retorici e bias rilevati

Struttura l'analisi in sezioni chiare con esempi specifici dal testo.",

            ["technology"] = @"Analizza questo articolo tecnologico con un approccio critico ed equilibrato.

1. ORIENTAMENTO TECNOLOGICO:
- Identifica se l'articolo mostra un bias tecno-ottimista o tecno-pessimista
- Valuta se vengono presentati sia benefici che rischi della tecnologia
- Evidenzia eventuali visioni deterministe che presentano sviluppi tecnologici come inevitabili

2. IMPLICAZIONI ETICHE E SOCIALI:
- Valuta se sono considerate le implicazioni etiche della tecnologia discussa
- Identifica se vengono analizzati gli impatti sociali oltre quelli tecnici o economici
- Evidenzia eventuali prospettive etiche mancanti o marginali

3. HYPE VS REALTÀ:
- Identifica eventuali esagerazioni sulle capacità attuali della tecnologia
- Valuta se vengono distinti prototipi/ricerche da prodotti maturi
- Evidenzia affermazioni non supportate da dati o ricerche verificabili

4. VISIONE SISTEMICA:
- Valuta se la tecnologia è presentata nel contesto degli ecosistemi esistenti
- Identifica se sono considerati i requisiti infrastrutturali, legali e sociali
- Evidenzia eventuali semplificazioni dell'adozione o dell'impatto tecnologico

5. CONCLUSIONE:
- Assegna un punteggio di oggettività da 0 a 100
- Sintetizza i principali elementi retorici e bias rilevati

Struttura l'analisi in sezioni chiare con esempi specifici dal testo.",

            ["world"] = @"Analizza questo articolo di notizie internazionali con un approccio critico e culturalmente sensibile.

1. CONTESTUALIZZAZIONE GEOPOLITICA:
- Valuta se gli eventi sono adeguatamente contestualizzati nella storia e cultura locale
- Identifica eventuali omissioni di contesto storico cruciale
- Evidenzia se vengono fornite sufficienti informazioni di background

2. BIAS ETNOCENTRICI:
- Identifica eventuali bias occidentali o etnocentrici nella narrazione
- Valuta se culture o paesi non occidentali sono descritti secondo standard occidentali
- Evidenzia eventuali giudizi impliciti basati su valori culturali specifici

3. STEREOTIPI REGIONALI:
- Identifica eventuali stereotipi su regioni, paesi o popolazioni
- Valuta se vengono usate generalizzazioni su gruppi nazionali o etnici
- Evidenzia rappresentazioni semplificate di realtà culturali complesse

4. PLURALITÀ DI PROSPETTIVE:
- Valuta se sono incluse voci e fonti locali
- Identifica quali prospettive sono privilegiate e quali marginali
- Evidenzia eventuali bias geopolitici che favoriscono certi interessi nazionali

5. CONCLUSIONE:
- Assegna un punteggio di oggettività da 0 a 100
- Sintetizza i principali elementi retorici e bias rilevati

Struttura l'analisi in sezioni chiare con esempi specifici dal testo.",

            // Default prompt per altre categorie
            ["default"] = @"Analizza questo articolo giornalistico con un approccio critico e imparziale.

1. BIAS E FRAMING:
- Identifica eventuali bias o orientamenti impliciti nel testo
- Valuta come viene inquadrato (framed) l'argomento principale
- Evidenzia eventuali prospettive privilegiate o marginali

2. TECNICHE RETORICHE:
- Identifica le principali tecniche retoriche utilizzate
- Valuta l'uso di linguaggio emotivo o persuasivo
- Evidenzia eventuali fallacie logiche o argomentative

3. EVIDENZE E FONTI:
- Valuta la qualità e varietà delle fonti citate
- Identifica eventuali affermazioni non supportate
- Evidenzia se vengono presentati dati in modo contestualizzato

4. PROSPETTIVE ALTERNATIVE:
- Suggerisci quali prospettive o informazioni mancano
- Proponi come l'articolo potrebbe essere più bilanciato
- Identifica informazioni di contesto rilevanti omesse

5. CONCLUSIONE:
- Assegna un punteggio di oggettività da 0 a 100
- Sintetizza i principali elementi retorici e bias rilevati

Struttura l'analisi in sezioni chiare con esempi specifici dal testo."
        };

        public string GetPromptForCategory(string category)
        {
            category = category?.ToLower() ?? "default";

            return _categoryPrompts.ContainsKey(category)
                ? _categoryPrompts[category]
                : _categoryPrompts["default"];
        }

        public void AddOrUpdatePrompt(string category, string prompt)
        {
            category = category.ToLower();

            if (_categoryPrompts.ContainsKey(category))
            {
                _categoryPrompts[category] = prompt;
            }
            else
            {
                _categoryPrompts.Add(category, prompt);
            }
        }
    }
}