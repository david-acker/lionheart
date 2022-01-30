## Lionheart
Extract your Robinhood trade data from your trade confirmation emails.

### Background
Currently Robinhood does not provide an easy or openly accessible way to export your stock/crypto trade history.
However, some of this information can be extracted from the trade confirmation emails sent by Robinhood.

Shortly after a trade has executed, Robinhood sends a confirmation email which contains basic information 
about the executed trade, such as:
* The instrument (stock ticker, name of cryptocurrency).
* The time of execution.
* The quantity.
* The average cost per unit.
* The notional value.

#### Email Subject Lines
For most stock and crypto trades, the confirmation emails sent by Robinhood have standard subject lines
which allows for easy identification and filtering.

| Type   | Subject Line                   |
|--------|--------------------------------|
| Stock  | "Your order has been executed" |
| Crypto | "Order Executed"               |

#### Email Body Templates
For stock trades, two different emails templates appear to be used depending on if the
quanity traded is fractional or in a whole share amount. For crypto trades, there only
appears to be a single email template used.

### Overall Process
Setting Up Email Fowarding:
1. The user creates an account and is assigned a unique alphanumeric access key.
2. The user sets up email forwarding filter(s) for the trade confirmation emails:
    - From Address: The Robinhood source email (noreply<span>@</span>robinhood.com). 
    - To Address: The centralized collection email plus addressed with the user's access key (i.e., `<local-part>+<access-key>@<domain>`).
    - Subject: The expected subject line(s) of the trade confirmation emails.
  
Email Processing:
1. The sender's address on the email is used to create/retrieve a record of the sender.
    - Could be expanded to help monitor of email traffic, identify spam senders, etc.
2. The sender's email address is used to retrieve the associated account, if one exists.
3. The email is verified by comparing the access key (extracted from the recipient address) to the account's access key.
4. The trade data is extracted from the email body.
    - The email is matched to the correct template using regular expressions.
    - The individual data points (e.g., ticker, notional, quantity, etc.) are retrieved using capture groups.
5. The resulting trade is validated and stored in the database.

### Limitations

#### Recurring Investments:
Recurring investments (stock and crypto) are not currently supported as their confirmation emails 
do not include the datetime at which the trade was executed.

#### Options Trades:
Option Trades are not currently supported as their confirmation emails lack some information
about the specific option contract that was traded (e.g., strike, expiration).
